using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̍s���N���X�B���̍U����ҋ@
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
