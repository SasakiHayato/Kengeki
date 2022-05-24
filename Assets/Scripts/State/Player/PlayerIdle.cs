using UnityEngine;
using System;
using StateMachine;

/// <summary>
/// PlayerのIdleステート
/// </summary>

public class PlayerIdle : State
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
        _player.Anim.Play("Idle_ver_B");
        _player.CharaData.UpdateSpeed(_player.CharaData.DefaultSpeed);
    }

    public override void Run()
    {
        _player.Move((Vector2)GamePadInputter.Instance.PlayerGetValue(GamePadInputter.ValueType.PlayerMove));
        _player.Rotate(Vector3.zero);
    }

    public override Enum Exit()
    {
        if (!_physicsBase.IsGround) return StateManager.ExitChangeState(Player.State.Float);

        if ((Vector2)GamePadInputter.Instance.PlayerGetValue(GamePadInputter.ValueType.PlayerMove) != Vector2.zero)
        {
            return Player.State.Move;
        }
        else
        {
            return Player.State.Idle;
        }
    }
}
