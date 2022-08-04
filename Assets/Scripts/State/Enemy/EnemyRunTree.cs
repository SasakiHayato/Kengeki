using UnityEngine;
using StateMachine;
using System;
using BehaviourTree;

/// <summary>
/// Enemy‚Ì’Êí‚ÌState
/// </summary>

public class EnemyRunTree : State
{
    BehaviourTreeUser _treeUser;
    PhysicsBase _physicsBase;

    public override void SetUp(GameObject user)
    {
        _treeUser = user.GetComponent<BehaviourTreeUser>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _treeUser.SetRunRequest(true);
    }

    public override void Run()
    {
        
    }

    public override Enum Exit()
    {
        if (_physicsBase.IsForce)
        {
            _treeUser.SetRunRequest(false);
            return EnemyBase.State.Nomotion;
        }
        else return EnemyBase.State.RunTree;
    }
}
