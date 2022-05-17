using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    List<Data> _datas;
    List<MapCreater.RoomData.Room> _roomList;

    public int RoomCount => _roomList.Count;
    public Data GetData(int id) => _datas[id];

    public void SetRoomList(List<MapCreater.RoomData.Room> rooms)
    {
        _roomList = rooms;
    }

    public void SetUpData(EnemyDataTip enemyDataTip)
    {
        int id = 0;
        _datas = new List<Data>();

        foreach (MapCreater.RoomData.Room room in _roomList)
        {
            int randomID = Random.Range(0, enemyDataTip.DataLegth);

            Data data = new Data(room, enemyDataTip.GetDataTip(randomID), id);
            _datas.Add(data);

            id++;
        }
    }

    public void InstantiateAll()
    {
        foreach (Data data in _datas)
        {
            Instantiate(data);
        }
    }

    public void InstantiateAt(int roomID)
    {
        Instantiate(_datas[roomID]);
    }

    void Instantiate(Data data)
    {
        foreach (EnemyPath path in data.EnemyTip.EnemyPaths)
        {
            int x = Random.Range((int)data.Room.UpperLeftPos.x, (int)data.Room.UpperRightPos.x);
            int y = Random.Range((int)data.Room.UpperRightPos.y, (int)data.Room.BottomRight.y);

            GameObject obj = Object.Instantiate(GameManager.Instance.ObjectData.GetData(path.ToString()).Prefab);
            obj.transform.position = new Vector3(x, 1, y);

            EnemyBase enemyBase = obj.GetComponent<EnemyBase>();
            enemyBase.SetRoomID(data.ID);
        }
    }

    public class Data
    {
        public int ID { get; private set; }
        public MapCreater.RoomData.Room Room { get; private set; }
        public EnemyDataTip.DataTip EnemyTip { get; private set; }

        public Data(MapCreater.RoomData.Room room, EnemyDataTip.DataTip tip, int id)
        {
            ID = id;
            Room = room;
            EnemyTip = tip;
        }
    }
}
