using UnityEngine;
using System;

/// <summary>
/// PlayerのIdleステート
/// </summary>

public class PlayerIdle : StateMachine.State
{
    public override void SetUp(GameObject user)
    {
        
    }

    public override void Entry()
    {
        
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
