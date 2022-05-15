using System;
using UnityEngine;
using StateMachine;

public class PlayerAttack : State
{
    Player _player;
    AttackSetting _attackSetting;
    PhysicsBase _physicsBase;

    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
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
        if (_player.Anim.EndCurrentAnimNormalizeTime)
        {
            Debug.Log("a");
        }
    }

    public override Enum Exit()
    {
        if (_player.Anim.EndCurrentAnimNormalizeTime)
        {
            _attackSetting.InitalizeID();
            return Player.State.Idle;
        }
        else return Player.State.Attack;
    }

}
