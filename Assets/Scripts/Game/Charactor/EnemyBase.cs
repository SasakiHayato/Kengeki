using UnityEngine;
using StateMachine;

/// <summary>
/// Enemy‚ÌŠî’êƒNƒ‰ƒX
/// </summary>

[RequireComponent(typeof(BehaviourTreeUser))]
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
    string _itemPath;
    
    protected StateManager StateManager { get; private set; }

    protected override void SetUp()
    {
        base.SetUp();

        StateManager = new StateManager(gameObject);
        StateManager.AddState(new EnemyRunTree(), State.RunTree)
            .AddState(new EnemyNomotion(), State.Nomotion)
            .RunRequest(true, State.RunTree);

        GetComponentInChildren<EnemyCanvas>().SetUp();

        GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager)).UpdateStatus(this);
    }

    protected override void DestoryRequest()
    {
        GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager)).RemoveEnemyEvent(RoomID, this);

        BaseUI.Instance.CallBack("GameUI", "Text", new object[] { GameManager.Instance.TextData.Request("GameMSG", 1) });
        GameManager.Instance.GetManager<ItemManager>(nameof(ItemManager)).SpawnRequest(_itemPath, transform);

        GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager)).Request(SoundType.SE, "Dead");
        
        base.DestoryRequest();
    }

    public void SetRoomID(int id) => RoomID = id;
    public void SetItemPath(string path) => _itemPath = path;
}
