using UnityEngine;
using BehaviourTree;

[RequireComponent(typeof(TreeManager))]
public abstract class EnemyBase : CharaBase
{
    Vector3 _moveDir;
    public Vector3 MoveDir
    {
        protected get
        {
            return _moveDir.normalized;
        }

        set
        {
            _moveDir = value;
        }
    }

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
