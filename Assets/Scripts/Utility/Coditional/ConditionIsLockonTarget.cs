using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの条件クラス。ロックオン対象がいるかどうか
/// </summary>

public class ConditionIsLockonTarget : BehaviourConditional
{
    protected override void Setup(GameObject user)
    {
        
    }

    protected override bool Try()
    {
        return GameManager.Instance.LockonTarget != null ? true : false;
    }

    protected override void Initialize()
    {
        
    }
}