using UnityEngine;
using BehaviourTree;

/// <summary>
/// Pod‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class Pod : CharaBase
{
    TreeManager _tree;

    public Vector3 MoveDir { get; set; }

    protected override void SetUp()
    {
        base.SetUp();

        _tree = GetComponent<TreeManager>();
        _tree.SetUp();
    }

    void Update()
    {
        _tree.TreeUpdate();

        Vector3 move = Vector3.Scale(MoveDir * CharaData.Speed, PhysicsBase.Gravity);
        RB.velocity = move;
    }
}
