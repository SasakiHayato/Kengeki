using System;
using UnityEngine;

public class NormalCm : StateMachine.State
{
    float _horizontalAngle;
    float _virticalAngle;

    Vector3 _setPos;

    CmManager _cmManager;
    
    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
    }

    public override void Entry()
    {
        
    }

    public override void Run()
    {
        Vector2 input = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.CmMove);

        Horizontal(input.x);
        //Virtical(input.y);

        _cmManager.CmData.Position = _setPos;
    }

    void Horizontal(float input)
    {
        if (Mathf.Abs(input) > _cmManager.CmData.DeadInput)
        {
            _horizontalAngle += input;
        }

        float rad = (_horizontalAngle / _cmManager.CmData.Sensitivity) * Mathf.Deg2Rad;

        _setPos.x = Mathf.Cos(rad);
        _setPos.z = Mathf.Sin(rad);
    }

    void Virtical(float input)
    {
        if (Mathf.Abs(input) > _cmManager.CmData.DeadInput)
        {
            _virticalAngle += input;
        }

        _setPos.y = _horizontalAngle;
    }

    public override Enum Exit()
    {
        return CmManager.State.Normal;
    }
}
