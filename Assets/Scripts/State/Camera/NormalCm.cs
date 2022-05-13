using System;
using UnityEngine;

public class NormalCm : StateMachine.State
{
    float _horizontalAngle;
    float _virticalAngle;

    Vector3 _setPos;

    CmManager _cmManager;
    Transform _cm;

    const int Circumference = 360;
    const float MaxAngle = 107;

    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
        _cm = user.transform;
    }

    public override void Entry()
    {
        
    }

    public override void Run()
    {
        Vector2 getVal = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.CmMove);
        Vector2 input = getVal.normalized;
        
        Horizontal(input.x);
        Virtical(input.y);

        _cmManager.CmData.NormalizePosition = _setPos;
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
        return CmManager.State.Normal;
    }
}
