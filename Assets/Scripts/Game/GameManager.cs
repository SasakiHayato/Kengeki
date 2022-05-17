using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SingletonAttribute;

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

    public ObjectDataBase ObjectData { get; private set; }
    public FieldObjectData FieldObject { get; private set; }

    public MapData MapData { get; private set; }

    public override void SetUp()
    {
        GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
        ObjectData = Resources.Load<ObjectDataBase>("ObjectDataBase");
        FieldObject = new FieldObjectData();

        _managerList = new List<IManager>();
    }

    public void SetMapData()
    {
        MapData = Object.FindObjectOfType<MapCreater>().Create();
        int randomRoomID = Random.Range(0, MapData.RoomList.Count);

        GameObject player = Object.Instantiate(ObjectData.GetData("Player").Prefab);
        Vector2 pos = MapData.RoomList[randomRoomID].CenterPos;
        player.transform.position = new Vector3(pos.x, 10, pos.y);
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
}
