using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree�̏����N���X�B���b�N�I���Ώۂ����邩�ǂ���
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