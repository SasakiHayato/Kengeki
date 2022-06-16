using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class Pod : CharaBase
{
    TreeManager _tree;

    public Vector3 MoveDir { get; set; }

    protected override void SetUp()
    {
        base.SetUp();
        _tree = GetComponent<TreeManager>();
    }

    void Update()
    {
        _tree.TreeUpdate();

        Vector3 move = Vector3.Scale(MoveDir * CharaData.Speed, PhysicsBase.Gravity);
        RB.velocity = move;
    }
}
