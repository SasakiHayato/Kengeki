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
        Vector2 input = (Vector2)GamePadInputter.Instance.PlayerGetValue(GamePadInputter.ValueType.PlayerMove);

        _player.Move(input);
        Rotate(input);
    }

    void Rotate(Vector2 input)
    {
        Vector3 forward = Camera.main.transform.forward * input.y;
        Vector3 right = Camera.main.transform.right * input.x;

        Vector3 set = forward + right;
        
        _player.Rotate(set);
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
