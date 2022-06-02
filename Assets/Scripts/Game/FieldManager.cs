using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldManager : MonoBehaviour, IManager
{
    public List<MapCreater.RoomData> RoomList { get; private set; }

    void Start()
    {
        MapCreater newMapCreater = FindObjectOfType<MapCreater>();
        newMapCreater.Create();
        RoomList = newMapCreater.RoomList;

        InstancePlayer();

        for (int i = 0; i < RoomList.Count; i++)
        {
            if (!RoomList[i].IsSet) InstanceEnemy(RoomList[i]);
        }

        GameManager.Instance.AddManager(this);
    }

    void InstancePlayer()
    {
        int randomID = Random.Range(0, RoomList.Count);
        GameObject player = GameManager.Instance.Player;
        MapCreater.RoomData data = RoomList[randomID];
        Vector3 setPos = data.Position.Center;
        setPos.y += 5;
        player.transform.position = setPos;

        data.IsRoomSetUp(true);
    }

    void InstanceEnemy(MapCreater.RoomData data)
    {
        int randomTip = Random.Range(0, GameManager.Instance.EnemyDataTip.DataLegth);

        EnemyDataTip.DataTip tip = GameManager.Instance.EnemyDataTip.GetDataTip(randomTip);

        foreach (EnemyPath path in tip.EnemyPaths)
        {
            GameObject obj = Instantiate(GameManager.Instance.ObjectData.GetData(path.ToString()).Prefab);
            Vector3 setPos = data.Position.Center;
            setPos.y += 5;
            obj.transform.position = setPos;

            obj.GetComponent<EnemyBase>().SetRoomID(data.Info.ID);
        }

        data.IsRoomSetUp(true);
    }

    public MapCreater.RoomData GetRoomData(int id) => RoomList.FirstOrDefault(r => r.Info.ID == id);

    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => nameof(FieldManager);
}
