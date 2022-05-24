using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Map
{
    public class NewMapCreater : MonoBehaviour
    {
        enum CellType
        {
            Room,
            Load,
            Wall,

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
            public Vector3 Position { get; set; }
            public CellType CellType { get; set; }
        }

        class RoomData
        {
            public int ID;
            public int Range;
            public PositionData Position;

            public bool IsSet { get; private set; }
            public void IsRoomSetUp(bool isSetup) => IsSet = isSetup;

            public class PositionData
            {
                public PositionData(int x, int y, int z, int range)
                {
                    Center = new Vector3((x + range) / 2, y, (z + range) / 2);
                    UpperLeft = new Vector3(x, y, z);
                    UpperRight = new Vector3(x + range, y, z);
                    BottomLeft = new Vector3(x, y, z + range);
                    BottomRight = new Vector3(x + range, y, z + range);
                }

                public Vector3 Center { get; private set; }
                public Vector3 UpperLeft { get; private set; }
                public Vector3 UpperRight { get; private set; }
                public Vector3 BottomLeft { get; private set; }
                public Vector3 BottomRight { get; private set; }
            }
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
        List<LoadData> _loadDatas;

        int _roomID;

        void Start()
        {
            Create();
        }

        void Create()
        {
            Initalize();

            for (int i = 0; i < _roomCount; i++)
            {
                CreateRoom();
            }

            CreateLoad();

            View();
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
            RoomData roomData = new RoomData
            {
                ID = _roomID,
                Range = roomRange,
                Position = position
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

        void CreateLoad()
        {
            RoomData room = _roomDatas[0];
            CreateLoadUp((room.Position.BottomRight + room.Position.UpperRight) / 2);
            CreateLoadDown((room.Position.UpperLeft + room.Position.BottomLeft) / 2);
        }

        void CreateLoadUp(Vector3 pos)
        {
            for (int x = (int)pos.x; x < _horizontalRange; x++)
            {
                for (int y = (int)pos.z; y < (int)pos.z + _loadWidth; y++)
                {
                    _cellDatas[x, y].CellType = CellType.Load;
                }
            }
        }

        void CreateLoadDown(Vector3 pos)
        {
            for (int x = (int)pos.x - 1; x > 0; x--)
            {
                for (int y = (int)pos.z; y < (int)pos.z + _loadWidth; y++)
                {
                    _cellDatas[x, y].CellType = CellType.Load;
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

                    GameObject tip = Instantiate(_tipDatas.FirstOrDefault(t => t.CellType == cellData.CellType).TipPrefab);
                    tip.transform.position = cellData.Position;
                }
            }
        }

        void Initalize()
        {
            _cellDatas = new CellData[_horizontalRange, _verticalRange];
            _roomDatas = new List<RoomData>();
            _loadDatas = new List<LoadData>();

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

}