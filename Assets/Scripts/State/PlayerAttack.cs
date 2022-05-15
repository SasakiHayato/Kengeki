using System;
using UnityEngine;

public class PlayerAttack : StateMachine.State
{
    AttackSetting _attackSetting;
    PhysicsBase _physicsBase;

    public override void SetUp(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry()
    {
        AttackType type;

        if (_physicsBase.IsGround) type = AttackType.Weak;
        else type = AttackType.Float;

        _attackSetting.Request(type);
    }
    
    public override void Run()
    {
        
    }

    public override Enum Exit()
    {
        return Player.State.Attack;
    }

}
