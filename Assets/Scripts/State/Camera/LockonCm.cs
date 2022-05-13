using System;
using UnityEngine;

/// <summary>
/// ÉçÉbÉNÉIÉìéûÇÃCameraState
/// </summary>

public class LockonCm : StateMachine.State
{
    CmManager _cmManager;

    float _currentRate;
    float _lerpTimer;
    float _saveSign;
    
    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
    }

    public override void Entry()
    {
        _cmManager.ViewTarget = GameManager.Instance.LockonTarget;
        _currentRate = 0;
        _lerpTimer = 0;
    }

    public override void Run()
    {
        Vector2 getVal = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove);

        Vector3 forward = _cmManager.CmData.User.position - _cmManager.ViewTarget.position;
        Vector3 offset = Camera.main.transform.right * SetLerp(getVal.x) + Vector3.up;
        _cmManager.CmData.NormalizePosition = forward + offset;
    }

    float SetLerp(float input)
    {
        _lerpTimer += Time.deltaTime;

        float sign = Mathf.Sign(input) * -1;
        float rate = Mathf.Lerp(_currentRate, sign, _lerpTimer);

        if ((int)_saveSign != (int)sign)
        {
            _lerpTimer = 0;
            _currentRate = sign;
        }

        _currentRate = rate;
        _saveSign = sign;

        return rate;
    }

    public override Enum Exit()
    {
        if (GameManager.Instance.LockonTarget != null) return CmManager.State.Lockon;
        else return CmManager.State.Normal;
    }
}
