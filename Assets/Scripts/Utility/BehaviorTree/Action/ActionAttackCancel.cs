using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。攻撃のCancelをリクエスト
/// </summary>

public class ActionAttackCancel : IAction
{
    AttackSetting _attackSetting;

    public void SetUp(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    public bool Execute()
    {
        _attackSetting.Cancel();
        return true;
    }

    public void InitParam()
    {
        
    }
}
