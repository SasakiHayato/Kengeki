using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。次の攻撃を待機
/// </summary>

public class ActionWaitNextAttack : Action
{
    AttackSetting _attackSetting;

    protected override void Setup(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    protected override bool Execute()
    {
        return _attackSetting.IsNextInput;
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
