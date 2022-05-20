using UnityEngine;
using StateMachine;
using System;

public class EnemyNomotion : State
{
    PhysicsBase _physicsBase;

    float _timer;

    const float DurationTime = 0.5f;

    public override void SetUp(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _timer = 0;
        Debug.Log("Nomotion");
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
