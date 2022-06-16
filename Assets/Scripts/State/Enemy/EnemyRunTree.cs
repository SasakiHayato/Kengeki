using UnityEngine;
using StateMachine;
using BehaviourTree;
using System;

/// <summary>
/// Enemy‚Ì’Êí‚ÌState
/// </summary>

public class EnemyRunTree : State
{
    TreeManager _tree;
    PhysicsBase _physicsBase;

    public override void SetUp(GameObject user)
    {
        _tree = user.GetComponent<TreeManager>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _tree.IsRun = true;
    }

    public override void Run()
    {
        _tree.TreeUpdate();
    }

    public override Enum Exit()
    {
        if (_physicsBase.IsForce)
        {
            _tree.IsRun = false;
            return EnemyBase.State.Nomotion;
        }
        else return EnemyBase.State.RunTree;
    }
}
