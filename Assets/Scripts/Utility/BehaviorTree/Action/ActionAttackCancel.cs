using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̍s���N���X�B�U����Cancel�����N�G�X�g
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
