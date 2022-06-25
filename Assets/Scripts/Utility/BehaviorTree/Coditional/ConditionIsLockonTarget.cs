using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの条件クラス。ロックオン対象がいるかどうか
/// </summary>

public class ConditionIsLockonTarget : IConditional
{
    public void SetUp(GameObject user)
    {

    }

    public bool Try()
    {
        return GameManager.Instance.LockonTarget != null ? true : false;
    }

    public void InitParam()
    {
        
    }
}