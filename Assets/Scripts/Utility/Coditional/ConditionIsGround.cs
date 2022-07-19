using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの条件クラス。対象が地面にいるかどうかの成否
/// </summary>

public class ConditionIsGround : BehaviourConditional
{
    PhysicsBase _physicsBase;

    protected override void Setup(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    protected override bool Try()
    {
        return _physicsBase.IsGround;
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
