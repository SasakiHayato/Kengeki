using UnityEngine;
using System;
using StateMachine;

/// <summary>
/// PlayerのMoveステート
/// </summary>

public class PlayerMove : State
{
    Player _player;
    PhysicsBase _physicsBase;
   
    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        

        if (beforeStatePath == Player.State.Dodge.ToString())
        {
            _player.Anim.Play("Run_Fast_ver_B");
            _player.CharaData.UpdateSpeed(_player.CharaData.DefaultSpeed * 1.4f);
        }
        else
        {
            _player.Anim.Play("Run_ver_B");
            _player.CharaData.UpdateSpeed(_player.CharaData.DefaultSpeed);
        }
    }

    public override void Run()
    {
        _player.Move((Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove));
    }

    public override Enum Exit()
    {
        if (!_physicsBase.IsGround) return StateManager.ExitChangeState(Player.State.Float);

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
