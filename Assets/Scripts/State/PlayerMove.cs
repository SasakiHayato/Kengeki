using UnityEngine;
using System;

/// <summary>
/// PlayerのMoveステート
/// </summary>

public class PlayerMove : StateMachine.State
{
    Player _player;

    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
    }

    public override void Entry()
    {
        _player.Anim.Play("Run_ver_B");
    }

    public override void Run()
    {

    }

    public override Enum Exit()
    {
        if ((Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove) != Vector2.zero)
        {
            return Player.State.Move;
        }
        else
        {
            return Player.State.Idle;
        }
    }
}
