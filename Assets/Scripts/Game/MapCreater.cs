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
    }

    [System.Serializable]
    class RoomData
    {
        [SerializeField] int _roomCount;
        [SerializeField] int _minRoomRange;
        [SerializeField] int _maxRoomRange;
        [SerializeField] int _roomDistance;

        public int RoomCount => _roomCount;
        public int GetRandomRoomRange => Random.Range(_minRoomRange, _maxRoomRange);
        public int RoomDistance => _roomDistance;

        public List<Room> Rooms { get; private set; } = new List<Room>();

        public class Room
        {
            public Vector2 UpperRightPos { get; private set; }
            public Vector2 UpperLeftPos { get; private set; }
            public Vector2 BottomRight { get; private set; }
            public Vector2 BottomLeft { get; private set; }

            public void SetData(int minX, int minY, int range)
            {
                UpperRightPos = new Vector2(minX + range, minY);
                UpperLeftPos = new Vector2(minX, minY);
                BottomRight = new Vector2(minX + range, minY + range);
                BottomLeft = new Vector2(minX, minY + range);
            }
        }
    }

    [System.Serializable]
    class CellData
    {
        [SerializeField] CellType _cellType;
        [SerializeField] GameObject _tip;

        public CellType CellType => _cellType;
        public GameObject Tip => _tip;
    }

    [SerializeField] int _horizontalRange;
    [SerializeField] int _verticalRange;
    [SerializeField] CellType _initCellType;
    [SerializeField] RoomData _roomData;
    [SerializeField] List<CellData> _cellDatas;

    CellType[,] _cells;

    void Start()
    {
        Create();
    }

    void Create()
    {
        Init();

        for (int i = 0; i < _roomData.RoomCount; i++)
        {
            CreateRoom();
        }

        CreateLoad();
        OverwriteRoom();
        CreateAroundWall();

        SetMap();
    }

    void CreateAroundWall()
    {
        for (int x = 0; x < _horizontalRange; x++)
        {
            _cells[x, 0] = CellType.Wall;
        }

        for (int x = 0; x < _horizontalRange; x++)
        {
            _cells[x, _verticalRange - 1] = CellType.Wall;
        }

        for (int y = 0; y < _verticalRange; y++)
        {
            _cells[0, y] = CellType.Wall;
        }

        for (int y = 0; y < _verticalRange; y++)
        {
            _cells[_horizontalRange - 1, y] = CellType.Wall;
        }
    }

    void CreateRoom()
    {
        int roomRange = _roomData.GetRandomRoomRange;

        int setCellX = Random.Range(1, _horizontalRange - roomRange);
        int setCellY = Random.Range(1, _verticalRange - roomRange);

        if (!CheckIsCreateRoom(setCellX, setCellY, roomRange))
        {
            CreateRoom();
            return;
        }
       
        for (int x = setCellX; x < roomRange + setCellX; x++)
        {
            for (int y = setCellY; y < roomRange + setCellY; y++)
            {
                _cells[x, y] = CellType.Room;

                RoomData.Room room = new RoomData.Room();
                room.SetData(setCellX, setCellY, roomRange);

                _roomData.Rooms.Add(room);
            }
        }
    }

    bool CheckIsCreateRoom(int x, int y, int range)
    {
        if (_roomData.Rooms.Count <= 0) return true;

        foreach (RoomData.Room room in _roomData.Rooms)
        {
            int upperRigthX = (int)room.UpperRightPos.x + _roomData.RoomDistance;
            int upperLeftX = (int)room.UpperLeftPos.x - _roomData.RoomDistance;

            int bottomLeftY = (int)room.BottomLeft.y + _roomData.RoomDistance;
            int upperLeftY = (int)room.UpperLeftPos.y - _roomData.RoomDistance;

            if (upperRigthX >= x && upperLeftX <= x + range)
            {
                if (bottomLeftY >= y && upperLeftY <= y + range)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void OverwriteRoom()
    {
        foreach (RoomData.Room room in _roomData.Rooms)
        {
            for (int x = (int)room.UpperLeftPos.x; x < (int)room.UpperRightPos.x; x++)
            {
                for (int y = (int)room.UpperLeftPos.y; y < (int)room.BottomRight.y; y++)
                {
                    _cells[x, y] = CellType.Room;
                }
            }
        }
    }

    void CreateLoad()
    {
        foreach (RoomData.Room room in _roomData.Rooms)
        {
            int virtical = (int)(room.UpperLeftPos.x + room.UpperRightPos.x) / 2;
            CreateLoadVirtical(virtical);

            int horizontal = (int)(room.UpperLeftPos.y + room.BottomLeft.y) / 2;
            CreateLoadHorizontal(horizontal);
        }
    }

    void CreateLoadVirtical(int cell)
    {
        for (int x = cell; x < cell + 1; x++)
        {
            for (int y = 0; y < _verticalRange; y++)
            {
                _cells[x, y] = CellType.Load;
            }
        }
    }

    void CreateLoadHorizontal(int cell)
    {
        for (int x = 0; x < _horizontalRange; x++)
        {
            for (int y = cell; y < cell + 1; y++)
            {
                _cells[x, y] = CellType.Load;
            }
        }
    }

    void SetMap()
    {
        for (int x = 0; x < _horizontalRange; x++)
        {
            for (int y = 0; y < _verticalRange; y++)
            {
                SetCell(_cells[x, y], x, y);
            }
        }
    }

    void SetCell(CellType type, int x, int y)
    {
        GameObject tip = Instantiate(_cellDatas.FirstOrDefault(c => c.CellType == type).Tip);

        tip.transform.position = new Vector3(x, 0, y);
        tip.transform.SetParent(transform);
    }

    void Init()
    {
        _cells = new CellType[_horizontalRange, _verticalRange];
        for (int x = 0; x < _horizontalRange; x++)
        {
            for (int y = 0; y < _verticalRange; y++)
            {
                _cells[x, y] = _initCellType;
            }
        }
    }
}
