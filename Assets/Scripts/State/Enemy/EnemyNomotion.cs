using UnityEngine;
using StateMachine;
using System;

/// <summary>
/// UŒ‚‚ğó‚¯‚½Û‚ÌEnemy‚ÌState
/// </summary>

public class EnemyNomotion : State
{
    PhysicsBase _physicsBase;
    EnemyBase _enemyBase;

    float _timer;

    const float DurationTime = 0.0f;

    public override void SetUp(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
        _enemyBase = user.GetComponent<EnemyBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _timer = 0;
        _enemyBase.MoveDir = Vector3.up;
        _enemyBase.CharaData.UpdateSpeed(1);
    }

    public override void Run()
    {
        if (!_physicsBase.IsForce)
        {
            _timer += Time.deltaTime;
        }
    }

    public override Enum Exit()
    {
        if (_timer > DurationTime)
        {
            _enemyBase.MoveDir = Vector3.zero;
            _enemyBase.CharaData.UpdateSpeed(_enemyBase.CharaData.DefaultSpeed);
            return EnemyBase.State.RunTree;
        }
        else return EnemyBase.State.Nomotion;
    }
}
