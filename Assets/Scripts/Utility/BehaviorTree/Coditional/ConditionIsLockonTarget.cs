using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̏����N���X�B���b�N�I���Ώۂ����邩�ǂ���
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