using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldManager : MonoBehaviour, IManager
{
    [SerializeField] MapCreater _creater;
    public List<MapCreater.RoomData> RoomList { get; private set; }

    const float ConstYPosition = 5f;

    void Start()
    {
        _creater.Create();
        RoomList = _creater.RoomList;

        InstancePlayer();
        _creater.SetTeleportFrag();

        for (int i = 0; i < RoomList.Count; i++)
        {
            if (!RoomList[i].IsSetUp) InstanceEnemy(RoomList[i]);
        }

        GameManager.Instance.AddManager(this);
    }

    void InstancePlayer()
    {
        int randomID = Random.Range(0, RoomList.Count);
        GameObject player = GameManager.Instance.Player;
        MapCreater.RoomData data = RoomList[randomID];
        Vector3 setPos = data.Position.Center;
        setPos.y = ConstYPosition;
        player.transform.position = setPos;

        data.Info.IsSetTeleporter(false);
        data.IsRoomSetUp(true);
    }

    void InstanceEnemy(MapCreater.RoomData data)
    {
        int randomTip = Random.Range(0, GameManager.Instance.EnemyDataTip.DataLegth);

        EnemyDataTip.DataTip tip = GameManager.Instance.EnemyDataTip.GetDataTip(randomTip);

        foreach (EnemyPath path in tip.EnemyPaths)
        {
            GameObject obj = Instantiate(GameManager.Instance.ObjectData.GetData(path.ToString()).Prefab);

            float x = Random.Range(data.Position.UpperLeft.x, data.Position.BottomRight.x);
            float z = Random.Range(data.Position.UpperLeft.z, data.Position.BottomRight.z);

            Vector3 setPos = new Vector3(x, ConstYPosition, z);
            obj.transform.position = setPos;

            EnemyBase enemyBase = obj.GetComponent<EnemyBase>();
            enemyBase.SetRoomID(data.Info.ID);

            data.Info.AddEnemy(enemyBase);
        }

        data.IsRoomSetUp(true);
    }

    public void RemoveEnemyEvent(int roomID, EnemyBase enemyBase)
    {
        MapCreater.RoomData data = RoomList.FirstOrDefault(r => r.Info.ID == roomID);
        data.Info.RemoveEnemy(enemyBase);
        
        if (data.Info.SetTeleporter && data.Info.EnemyList.Count() <= 0)
        {
            _creater.SetTeleport();
        }
    }

    public MapCreater.RoomData GetRoomData(int id) => RoomList.FirstOrDefault(r => r.Info.ID == id);

    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => nameof(FieldManager);
}
