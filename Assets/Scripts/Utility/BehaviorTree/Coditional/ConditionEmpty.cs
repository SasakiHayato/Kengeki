using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̏����N���X�BDefault
/// </summary>

public class ConditionEmpty : IConditional
{
    public void SetUp(GameObject user)
    {

    }

    public bool Try()
    {
        return true;
    }

    public void InitParam()
    {
        
    }
}
