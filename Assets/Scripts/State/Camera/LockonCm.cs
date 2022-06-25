using System;
using UnityEngine;

/// <summary>
/// ÉçÉbÉNÉIÉìéûÇÃCameraState
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

        _cmManager.CmData.SaveState = CmManager.State.Lockon;

        BaseUI.Instance.CallBack("GameUI", "Lockon", new object[] { GameManager.Instance.LockonTarget });
    }

    public override void Run()
    {
        Vector3 forward = _cmManager.CmData.User.position - _cmManager.ViewTarget.position;
        Vector3 offset = Camera.main.transform.right + Vector3.up;

        _cmManager.CmData.Position = (forward + offset).normalized;
    }

    public override Enum Exit()
    {
        if (GameManager.Instance.LockonTarget != null)
        {

            return CmManager.State.Lockon;
        }
        else
        {
            BaseUI.Instance.CallBack("GameUI", "Lockon", new object[] { null });
            return CmManager.State.Normal;
        }
    }
}
