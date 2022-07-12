using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree�̍s���N���X�B�U����Cancel�����N�G�X�g
/// </summary>

public class ActionAttackCancel : Action
{
    AttackSetting _attackSetting;

    protected override void Setup(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    protected override bool Execute()
    {
        _attackSetting.Cancel();
        return true;
    }

    protected override void Initialize()
    {
        
    }
}
