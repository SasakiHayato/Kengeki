using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。次の攻撃を待機
/// </summary>

public class ActionWaitNextAttack : IAction
{
    AttackSetting _attackSetting;

    public void SetUp(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    public bool Execute()
    {
        return _attackSetting.IsNextInput;
    }

    public void InitParam()
    {
        
    }
}
