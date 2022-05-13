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

    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
        _cm = user.transform;
    }

    public override void Entry()
    {
        // VirticalAngle‚Ì‰Šú‰»
        Vector3 diff = _cm.position - _cmManager.CmData.User.position;
        float angle = Mathf.Atan2(diff.y, diff.z) * Mathf.Rad2Deg;
        _virticalAngle = angle;
    }

    public override void Run()
    {
        Vector2 input = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.CmMove);
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
        if (Mathf.Abs(input) > _cmManager.CmData.DeadInput)
        {
            _virticalAngle += input;
        }

        float rad = (_virticalAngle / _cmManager.CmData.Sensitivity) * Mathf.Deg2Rad;
        
        _setPos.y = Mathf.Cos(rad);
    }

    public override Enum Exit()
    {
        return CmManager.State.Normal;
    }
}
