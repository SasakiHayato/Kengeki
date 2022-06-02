using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapCreater : MonoBehaviour
{
    enum CellType
    {
        Room,
        Load,
        Wall,
        Teleporter,

        None,
    }

    [System.Serializable]
    class TipData
    {
        public CellType CellType;
        public GameObject TipPrefab;
    }

    class CellData
    {
        public GameObject Tip { get; set; }
        public Vector3 Position { get; set; }
        public CellType CellType { get; set; }
    }

    class LoadData
    {
        public int ID { get; set; }
        public List<Vector3> PositionList { get; set; }

        public bool IsConnect { get; private set; }

        public void SetConnect(bool isConnect) => IsConnect = isConnect;
    }

    [SerializeField] int _horizontalRange;
    [SerializeField] int _verticalRange;
    [SerializeField] int _roomCount;
    [SerializeField] int _minRoomRange;
    [SerializeField] int _maxRoomRange;
    [SerializeField] int _roomDistance;
    [SerializeField] int _loadWidth;
    [SerializeField] List<TipData> _tipDatas;

    CellData[,] _cellDatas;
    List<RoomData> _roomDatas;

    int _roomID;

    GameObject _parent;

    public List<RoomData> RoomList => _roomDatas;

    public void Create()
    {
        GameManager.Instance.AddFieldHierarchy();

        _parent = new GameObject("MapCells");

        Initalize();

        for (int i = 0; i < _roomCount; i++)
        {
            CreateRoom();
        }

        CreateLoad();
        OverwriteRoom();
        CreateWall();

        View();
    }

    public void SetTeleportFrag()
    {
        var list = RoomList.Where(r => !r.IsSetUp);
        int random = Random.Range(0, list.Count());

        RoomData.RoomInfo info = list.ToList()[random].Info;
        info.IsSetTeleporter(true);
    }

    public void SetTeleport()
    {
        RoomData data = RoomList.FirstOrDefault(r => r.Info.SetTeleporter);
        Vector3 cellPosition = data.Position.Center;

        CellData cellData = _cellDatas[(int)cellPosition.x, (int)cellPosition.z];
        cellData.CellType = CellType.Teleporter;

        Destroy(cellData.Tip);

        Set(cellData, _parent);
    }

    void CreateRoom()
    {
        int roomRange = Random.Range(_minRoomRange, _maxRoomRange);
        int setCellX = Random.Range(1, _horizontalRange - roomRange);
        int setCellY = Random.Range(1, _verticalRange - roomRange);

        if (!CheckIsCreateRoom(setCellX, setCellY, roomRange))
        {
            CreateRoom();
            return;
        }

        for (int x = setCellX; x < setCellX + roomRange; x++)
        {
            for (int y = setCellY; y < setCellY + roomRange; y++)
            {
                _cellDatas[x, y].CellType = CellType.Room;
            }
        }

        RoomData.PositionData position = new RoomData.PositionData(setCellX, 0, setCellY, roomRange);
        RoomData.RoomInfo info = new RoomData.RoomInfo(_roomID, roomRange, new Vector2(setCellX, setCellY));

        RoomData roomData = new RoomData()
        {
            Position = position,
            Info = info
        };

        _roomDatas.Add(roomData);

        _roomID++;
    }

    bool CheckIsCreateRoom(int x, int y, int range)
    {
        if (_roomDatas.Count <= 0) return true;

        foreach (RoomData room in _roomDatas)
        {
            if (room.Position.UpperRight.x + _roomDistance >= x && room.Position.UpperLeft.x - _roomDistance <= x + range)
            {
                if (room.Position.BottomLeft.z + _roomDistance >= y && room.Position.UpperLeft.z - _roomDistance <= y + range)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void CreateWall()
    {
        for (int x = 0; x < _horizontalRange; x++)
        {
            for (int y = 0; y < _verticalRange; y++)
            {
                if (_cellDatas[x, y].CellType == CellType.None)
                {
                    _cellDatas[x, y].CellType = CellType.Wall;
                }
            }
        }
    }

    void CreateLoad()
    {
        for (int x = 0; x < _roomCount; x++)
        {
            for (int y = x + 1; y < _roomCount; y++)
            {
                CheckIsCreateLoad(_roomDatas[x], _roomDatas[y]);
            }
        }
    }

    void CheckIsCreateLoad(RoomData data1, RoomData data2)
    {
        HorizontalLoad(data1, data2);
        VerticalLaod(data1, data2);
    }

    void HorizontalLoad(RoomData data1, RoomData data2)
    {
        // data1
        int range1 = data1.Info.Range;
        int left1 = (int)(data1.Position.Center.x - (range1 / 2));
        int right1 = (int)(data1.Position.Center.x + (range1 / 2));
        int center1 = (int)data1.Position.Center.z;

        // data2
        int range2 = data2.Info.Range;
        int left2 = (int)(data2.Position.Center.x - (range2 / 2));
        int right2 = (int)(data2.Position.Center.x + (range2 / 2));
        int center2 = (int)data2.Position.Center.z;

        int min = Mathf.Min(center1, center2);
        int max = Mathf.Max(center1, center2);

        if (left1 > right2)
        {
            CreateHorizontalLoad((left1 + right2) / 2, left1, center1);
            CreateHorizontalLoad(right2, (left1 + right2) / 2, center2);
            CreateVirticalLoad(min, max + 1, (left1 + right2) / 2);
        }

        if (left2 > right1)
        {
            CreateHorizontalLoad((left2 + right1) / 2, left2, center2);
            CreateHorizontalLoad(right1, (left2 + right1) / 2, center1);
            CreateVirticalLoad(min, max + 1, (left2 + right1) / 2);
        }
    }

    void VerticalLaod(RoomData data1, RoomData data2)
    {
        // data1
        int range1 = data1.Info.Range;
        int up1 = (int)(data1.Position.Center.z - (range1 / 2));
        int bottom1 = (int)(data1.Position.Center.z + (range1 / 2));
        int center1 = (int)data1.Position.Center.x;

        // data2
        int range2 = data2.Info.Range;
        int up2 = (int)(data2.Position.Center.z - (range2 / 2));
        int bottom2 = (int)(data2.Position.Center.z + (range2 / 2));
        int center2 = (int)data2.Position.Center.x;

        int min = Mathf.Min(center1, center2);
        int max = Mathf.Max(center1, center2);

        if (up1 > bottom2)
        {
            CreateVirticalLoad((bottom2 + up1) / 2, up1, center1);
            CreateVirticalLoad(bottom2, (bottom2 + up1) / 2, center2);
            CreateHorizontalLoad(min, max + 1, (bottom2 + up1) / 2);
        }

        if (up2 > bottom1)
        {
            CreateVirticalLoad((bottom1 + up2) / 2, up2, center2);
            CreateVirticalLoad(bottom1, (bottom1 + up2) / 2, center1);
            CreateHorizontalLoad(min, max + 1, (bottom1 + up2) / 2);
        }
    }

    void CreateHorizontalLoad(int startPos, int endPos, int constY)
    {
        for (int x = startPos; x < endPos; x++)
        {
            for (int y = constY; y < constY + _loadWidth; y++)
            {
                _cellDatas[x, y].CellType = CellType.Load;
            }
        }
    }

    void CreateVirticalLoad(int startPos, int endPos, int constX)
    {
        for (int x = constX; x < constX + _loadWidth; x++)
        {
            for (int y = startPos; y < endPos; y++)
            {
                _cellDatas[x, y].CellType = CellType.Load;
            }
        }
    }

    void OverwriteRoom()
    {
        foreach (RoomData room in _roomDatas)
        {
            int setCellX = (int)room.Info.CellIndex.x;
            int setCellY = (int)room.Info.CellIndex.y;

            for (int x = setCellX; x < setCellX + room.Info.Range; x++)
            {
                for (int y = setCellY; y < setCellY + room.Info.Range; y++)
                {
                    _cellDatas[x, y].CellType = CellType.Room;
                }
            }
        }
    }

    void View()
    {
        for (int x = 0; x < _horizontalRange; x++)
        {
            for (int y = 0; y < _verticalRange; y++)
            {
                CellData cellData = _cellDatas[x, y];

                if (cellData.CellType == CellType.None) continue;

                Set(cellData, _parent);
            }
        }
    }

    void Set(CellData cellData, GameObject parent)
    {
        GameObject tip = Instantiate(_tipDatas.FirstOrDefault(t => t.CellType == cellData.CellType).TipPrefab);
        tip.transform.position = cellData.Position;

        cellData.Tip = tip;

        tip.transform.SetParent(parent.transform);
    }

    void Initalize()
    {
        _cellDatas = new CellData[_horizontalRange, _verticalRange];
        _roomDatas = new List<RoomData>();

        _roomID = 0;

        for (int x = 0; x < _horizontalRange; x++)
        {
            for (int y = 0; y < _verticalRange; y++)
            {
                CellData cellData = new CellData
                {
                    CellType = CellType.None,
                    Position = new Vector3(x, 0, y)
                };
                _cellDatas[x, y] = cellData;
            }
        }
    }
}