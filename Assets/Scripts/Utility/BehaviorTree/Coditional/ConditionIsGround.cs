using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの条件クラス。対象が地面にいるかどうかの成否
/// </summary>

public class ConditionIsGround : IConditional
{
    PhysicsBase _physicsBase;

    public void SetUp(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public bool Try()
    {
        return _physicsBase.IsGround;
    }

    public void InitParam()
    {
        
    }
}
