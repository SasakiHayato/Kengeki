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

public interface IManager
{
    GameObject ManagerObject();
    string ManagerPath();
}

/// <summary>
/// ÉQÅ[ÉÄÇÃä«óùÉNÉâÉX
/// </summary>

public class GameManager : SingletonAttribute<GameManager>
{
    List<IManager> _managerList;
    
    public GameState CurrentGameState { get; private set; }

    public ObjectDataBase ObjectData { get; private set; }
    public FieldObjectData FieldObject { get; private set; }

    public MapData MapData { get; private set; }
    
    public GameObject Player { get; private set; }

    public override void SetUp()
    {
        ObjectData = Resources.Load<ObjectDataBase>("ObjectDataBase");
        FieldObject = new FieldObjectData();

        _managerList = new List<IManager>();
    }

    public void SetMapData()
    {
        MapData = Object.FindObjectOfType<MapCreater>().Create();
        MapData.SetUpData(Resources.Load<EnemyDataTip>("EnemyDataTip"));

        int randomRoomID = Random.Range(0, MapData.RoomCount);
        
        Player = Object.Instantiate(ObjectData.GetData("Player").Prefab);
        Vector2 pos = MapData.GetData(randomRoomID).Room.CenterPos;
        Player.transform.position = new Vector3(pos.x, 10, pos.y);
        
        MapData.InstantiateAll();
    }

    public Transform LockonTarget { get; set; }

    public void AddManager(IManager iManager)
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

    public void SetGameState(GameState gameState) => CurrentGameState = gameState;

    public void ChangeScene(string sceneName)
    {
        _managerList = new List<IManager>();
        GamePadInputter.Despose();
        SceneManager.LoadScene(sceneName);
    }
}
