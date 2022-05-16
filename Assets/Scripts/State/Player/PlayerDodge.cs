using UnityEngine;
using StateMachine;
using System;

public class PlayerDodge : State
{
    Player _player;

    Vector2 _saveInput;

    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
    }

    public override void Entry(string beforeStatePath)
    {
        Vector2 input = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove);

        if (input == Vector2.zero) _saveInput = Vector2.down;
        else _saveInput = input;

        _player.Anim.Play("Dodge_Left");
        _player.Data.UpdateSpeed(_player.Data.DefaultSpeed * 2);
    }

    public override void Run()
    {
        _player.Move(_saveInput);
    }

    public override Enum Exit()
    {
        if (_player.Anim.EndCurrentAnimNormalizeTime)
        {
            Vector2 input = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove);

            if (input == Vector2.zero) return Player.State.Idle;
            else return Player.State.Move;
        }
        else return Player.State.Dodge;
    }
}
