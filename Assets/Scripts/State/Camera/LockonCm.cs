using System;
using UnityEngine;

/// <summary>
/// ƒƒbƒNƒIƒ“‚ÌCameraState
/// </summary>

public class LockonCm : StateMachine.State
{
    CmManager _cmManager;

    const float VirticalRate = 3f;
    
    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
    }

    public override void Entry(string beforeStateName)
    {
        _cmManager.ViewTarget = GameManager.Instance.LockonTarget;
        _cmManager.CmData.VirticalRate = VirticalRate;
    }

    public override void Run()
    {
        Vector3 forward = _cmManager.CmData.User.position - _cmManager.ViewTarget.position;
        Vector3 offset = Camera.main.transform.right + Vector3.up;

        _cmManager.CmData.NormalizePosition = forward + offset;
    }

    public override Enum Exit()
    {
        if (GameManager.Instance.LockonTarget != null) return CmManager.State.Lockon;
        else return CmManager.State.Normal;
    }
}
