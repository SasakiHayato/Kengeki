using UnityEngine;
using BehaviourTree;

public class ConditionEntryRoom : IConditional
{
    Transform _player;

    Vector2 _minPos;
    Vector2 _maxPos;

    public void SetUp(GameObject user)
    {
        EnemyBase enemyBase = user.GetComponent<EnemyBase>();
        MapCreater.RoomData.Room room = GameManager.Instance.MapData.GetData(enemyBase.RoomID).Room;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;

        _minPos = room.UpperLeftPos;
        _maxPos = room.BottomRight;
    }

    public bool Try()
    {
        if (_minPos.x <= _player.position.x && _maxPos.x > _player.position.x)
        {
            if (_minPos.y <= _player.position.z && _maxPos.y > _player.position.z)
            {
                return true;
            }
        }


        return false;
    }

    public void InitParam()
    {
        
    }
}
