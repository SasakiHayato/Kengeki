using UnityEngine;
using BehaviourTree;

[RequireComponent(typeof(TreeManager))]
public class EnemyBase : CharaBase
{
    public int RoomID { get; private set; }
    protected TreeManager TreeManager { get; private set; }

    protected override void SetUp()
    {
        base.SetUp();



        TreeManager = GetComponent<TreeManager>();
        TreeManager.SetUp();
    }

    public void SetRoomID(int id) => RoomID = id;
}
