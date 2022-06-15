using UnityEngine;
using BehaviourTree;

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
