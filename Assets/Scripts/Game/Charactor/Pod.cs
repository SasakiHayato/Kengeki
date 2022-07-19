using UnityEngine;
using BehaviourTree;

/// <summary>
/// Pod�̊Ǘ��N���X
/// </summary>

[RequireComponent(typeof(BehaviourTreeUser))]
public class Pod : CharaBase
{
    public Vector3 MoveDir { get; set; }

    protected override void SetUp()
    {
        base.SetUp();
    }

    void Update()
    {
        Vector3 move = Vector3.Scale(MoveDir * CharaData.Speed, PhysicsBase.Gravity);
        RB.velocity = move;
    }
}
