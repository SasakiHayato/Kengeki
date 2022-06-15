using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using SingletonAttribute;

public enum GameState
{
    Title,
    InGame,

    None,
}

public enum MapType
{
    Normal,
    Arena,
}

/// <summary>
/// ÉQÅ[ÉÄÇÃä«óùÉNÉâÉX
/// </summary>

public class GameManager : SingletonAttribute<GameManager>
{
    List<ManagerBase> _managerList;

    public GameState CurrentGameState { get; private set; }
    public MapType CurrentMapType { get; private set; }

    public ObjectDataBase ObjectData { get; private set; }
    public FieldObjectData FieldObject { get; private set; }
    public List<EnemyDataTip> EnemyDataTipList { get; private set; }
    public TextDataGetter TextData { get; private set; }
    
    public Transform LockonTarget { get; set; }

    public int FieldHierarchy { get; private set; }

    public bool ManagerIsSetUp => _managerList.All(m => m.IsSetUp);

    public override void SetUp()
    {
        ObjectData = Resources.Load<ObjectDataBase>("ObjectDataBase");
        FieldObject = new FieldObjectData();

        _managerList = new List<ManagerBase>();

        EnemyDataTipList = new List<EnemyDataTip>();
        EnemyDataTipList.Add(Resources.Load<EnemyDataTip>("MobEnemyDataTip"));
        EnemyDataTipList.Add(Resources.Load<EnemyDataTip>("BossEnemyDataTip"));

        TextData = new TextDataGetter();
        TextData.SetUp();
    }

    public void AddFieldHierarchy() => FieldHierarchy++;

    public void AddManager(ManagerBase iManager)
    {
        if (_managerList.Count <= 0)
        {
            _managerList.Add(iManager);
            return;
        }

        if (_managerList.All(m => m.ManagerPath() != iManager.ManagerPath()))
        {
            _managerList.Add(iManager);
        }
    }

    public Manager GetManager<Manager>(string path) where Manager : class
    {
        GameObject obj = _managerList.FirstOrDefault(m => m.ManagerPath() == path).ManagerObject();
        Manager manager = obj.GetComponent<Manager>();

        return manager;
    }

    public void SetUpManager()
    {
        var sort = _managerList.OrderBy(m => m.Priority);
        sort.ToList().ForEach(m => m.SetUp());
    }

    public void SetGameState(GameState gameState) => CurrentGameState = gameState;
    public void SetMapType(MapType type) => CurrentMapType = type;

    public void ChangeScene(string sceneName)
    {
        _managerList = new List<ManagerBase>();
        FieldObject = new FieldObjectData();
        GamePadInputter.Despose();

        SceneManager.LoadScene(sceneName);
    }

    public void Init()
    {
        FieldHierarchy = 0;
        ItemDirectory.Instance.Dispose();
    }
}
