using UnityEngine;
using StateMachine;
using System;

public class EnemyNomotion : State
{
    PhysicsBase _physicsBase;
    EnemyBase _enemyBase;

    float _timer;

    const float DurationTime = 0.2f;

    public override void SetUp(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
        _enemyBase = user.GetComponent<EnemyBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _timer = 0;
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
        if (_timer > DurationTime) return EnemyBase.State.RunTree;
        else return EnemyBase.State.Nomotion;
    }
}
