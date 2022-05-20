using UnityEngine;
using StateMachine;
using BehaviourTree;
using System;

public class EnemyRunTree : State
{
    TreeManager _tree;
    PhysicsBase _physicsBase;
    EnemyBase _enemyBase;

    public override void SetUp(GameObject user)
    {
        _tree = user.GetComponent<TreeManager>();
        _physicsBase = user.GetComponent<PhysicsBase>();
        _enemyBase = user.GetComponent<EnemyBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _tree.IsRun = true;
        Debug.Log("Tree");
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
            _enemyBase.MoveDir = Vector3.one;
            return EnemyBase.State.Nomotion;
        }
        else return EnemyBase.State.RunTree;
    }
}
