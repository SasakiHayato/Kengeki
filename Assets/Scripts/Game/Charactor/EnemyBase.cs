using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

[RequireComponent(typeof(TreeManager))]
public class EnemyBase : CharaBase
{
    protected TreeManager TreeManager { get; private set; }

    protected override void SetUp()
    {
        base.SetUp();

        TreeManager = GetComponent<TreeManager>();
        TreeManager.SetUp();
    }
}
