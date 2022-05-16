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
        _attackSetting.SetUp();

        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        AttackType type;

        if (_physicsBase.IsGround) type = AttackType.Weak;
        else type = AttackType.Float;

        _attackSetting.Request(type);
        _player.CharaData.UpdateSpeed(_player.CharaData.DefaultSpeed);
    }
    
    public override void Run()
    {
        _player.Move((Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove));

        if (!_physicsBase.IsGround)
        {
            _physicsBase.InitializeTumer();
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
