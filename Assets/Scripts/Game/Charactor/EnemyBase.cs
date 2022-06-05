using UnityEngine;
using BehaviourTree;
using StateMachine;

[RequireComponent(typeof(TreeManager))]
public abstract class EnemyBase : CharaBase
{
    public enum State
    {
        RunTree,
        Nomotion,
    }

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

    protected StateManager StateManager { get; private set; }

    protected override void SetUp()
    {
        base.SetUp();

        TreeManager = GetComponent<TreeManager>();
        TreeManager.SetUp();

        StateManager = new StateManager(gameObject);
        StateManager.AddState(new EnemyRunTree(), State.RunTree)
            .AddState(new EnemyNomotion(), State.Nomotion)
            .RunRequest(true, State.RunTree);

        GetComponentInChildren<EnemyCanvas>().SetUp();
    }

    protected override void DestoryRequest()
    {
        GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager)).RemoveEnemyEvent(RoomID, this);
        GameManager.Instance.GetManager<ItemManager>(nameof(ItemManager)).SpawnRequest("Healer", transform);
        
        base.DestoryRequest();
    }

    public void SetRoomID(int id) => RoomID = id;
}
