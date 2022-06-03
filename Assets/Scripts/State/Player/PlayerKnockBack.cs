using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;
using System;

public class PlayerKnockBack : State
{
    PhysicsBase _physicsBase;
    Player _player;

    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        
    }

    public override void Run()
    {
        _player.Move(Vector3.up);
    }

    public override Enum Exit()
    {
        if (_physicsBase.IsForce) return Player.State.KnockBack;
        else return Player.State.Idle;
    }
}
