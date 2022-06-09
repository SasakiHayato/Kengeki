using UnityEngine;
using StateMachine;
using System;

public class PlayerDodge : State
{
    Player _player;
    PhysicsBase _physicsBase;

    Vector2 _saveInput;

    string _beforeStatePath;

    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        Vector2 input = GamePadInputter.Instance.PlayerGetValue(GamePadInputter.ValueType.PlayerMove);

        if (input == Vector2.zero) _saveInput = Vector2.down;
        else _saveInput = input;

        _beforeStatePath = beforeStatePath;

        _player.Anim.Play("Dodge_Left");
        _player.CharaData.UpdateSpeed(_player.CharaData.DefaultSpeed * 2);
    }

    public override void Run()
    {
        if (_beforeStatePath == Player.State.Float.ToString())
        {
            _physicsBase.InitializeTimer();
        }
        
        _player.Move(_saveInput);
    }

    public override Enum Exit()
    {
        if (_player.Anim.EndCurrentAnimNormalizeTime)
        {
            Vector2 input = GamePadInputter.Instance.PlayerGetValue(GamePadInputter.ValueType.PlayerMove);
            _player.IsDodge = false;

            if (input == Vector2.zero) return Player.State.Idle;
            else return Player.State.Move;
        }
        else return Player.State.Dodge;
    }
}
