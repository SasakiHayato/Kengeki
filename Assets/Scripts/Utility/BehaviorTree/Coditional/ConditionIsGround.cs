using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̏����N���X�B�Ώۂ��n�ʂɂ��邩�ǂ����̐���
/// </summary>

public class ConditionIsGround : IConditional
{
    PhysicsBase _physicsBase;

    public void SetUp(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public bool Try()
    {
        return _physicsBase.IsGround;
    }

    public void InitParam()
    {
        
    }
}
