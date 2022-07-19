using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree�̏����N���X�B�Ώۂ��n�ʂɂ��邩�ǂ����̐���
/// </summary>

public class ConditionIsGround : BehaviourConditional
{
    PhysicsBase _physicsBase;

    protected override void Setup(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    protected override bool Try()
    {
        return _physicsBase.IsGround;
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
