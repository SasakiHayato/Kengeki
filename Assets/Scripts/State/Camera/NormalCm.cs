using System;
using UnityEngine;

/// <summary>
/// �ʏ펞��CameraState
/// </summary>

public class NormalCm : StateMachine.State
{
    float _horizontalAngle;
    float _virticalAngle;

    Vector3 _setPos;

    CmManager _cmManager;
    
    const int Circumference = 360;
    const float MaxAngle = 107;

    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
    }

    public override void Entry(string beforeStateName)
    {
        _cmManager.ViewTarget = _cmManager.CmData.User;
        _cmManager.CmData.VirticalRate = float.MaxValue;
        _cmManager.CmData.SaveState = CmManager.State.Normal;
    }

    public override void Run()
    {
        Vector2 getVal = GamePadInputter.Instance.PlayerGetValue(GamePadInputter.ValueType.CmMove);
        Vector2 input = getVal.normalized;
        
        Horizontal(input.x * CmInputData.HorizontalInput);
        Virtical(input.y * CmInputData.VerticalInput);

        _cmManager.CmData.Position = _setPos.normalized;
    }

    void Horizontal(float input)
    {
        float angle = _horizontalAngle / _cmManager.CmData.Sensitivity;

        if (Mathf.Abs(input) > _cmManager.CmData.DeadInput)
        {
            _horizontalAngle += input;
        }

        float rad = angle * Mathf.Deg2Rad;

        _setPos.x = Mathf.Cos(rad);
        _setPos.z = Mathf.Sin(rad);

        if (Mathf.Abs(angle) >= Circumference) _horizontalAngle = 0;
    }

    void Virtical(float input)
    {
        float angle = _virticalAngle / _cmManager.CmData.Sensitivity;

        if (Mathf.Abs(input) > _cmManager.CmData.DeadInput)
        {
            _virticalAngle += input;
        }

        if (angle < 0)
        {
            angle = 0;
            _virticalAngle = 0;
        }

        if (Mathf.Abs(angle) > MaxAngle)
        {
            angle = MaxAngle;
            _virticalAngle = angle * _cmManager.CmData.Sensitivity;
        }

        float rad = angle * Mathf.Deg2Rad;
        
        _setPos.y = Mathf.Cos(rad);
    }

    public override Enum Exit()
    {
        if (GameManager.Instance.LockonTarget != null) return CmManager.State.Lockon;
        else return CmManager.State.Normal;
    }
}
