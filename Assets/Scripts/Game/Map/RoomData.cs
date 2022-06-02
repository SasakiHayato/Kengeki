using System.Collections.Generic;
using UnityEngine;

public class RoomData
{
    public PositionData Position { get; set; }
    public RoomInfo Info { get; set; }

    public bool IsSetUp { get; private set; }
    public void IsRoomSetUp(bool isSetup) => IsSetUp = isSetup;

    public class PositionData
    {
        public PositionData(int x, int y, int z, int range)
        {
            Center = new Vector3(x + (range / 2), y, z + (range / 2));
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

    public class RoomInfo
    {
        public int ID { get; private set; }
        public int Range { get; private set; }
        public Vector2 CellIndex { get; private set; }

        public bool SetTeleporter { get; private set; }

        public List<EnemyBase> EnemyList { get; private set; }

        public RoomInfo(int id, int range, Vector2 cellIndex)
        {
            ID = id;
            Range = range;
            CellIndex = cellIndex;

            EnemyList = new List<EnemyBase>();
        }

        public void IsSetTeleporter(bool isSet) => SetTeleporter = isSet;

        public void AddEnemy(EnemyBase enemyBase) => EnemyList.Add(enemyBase);
        public void RemoveEnemy(EnemyBase enemyBase) => EnemyList.Remove(enemyBase);
    }
}