using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̏����N���X�BPlayer���Ώۂ�Room�ɂ��邩�ǂ����̐���
/// </summary>

public class ConditionEntryRoom : IConditional
{
    Transform _player;

    Vector3 _minPos;
    Vector3 _maxPos;

    public void SetUp(GameObject user)
    {
        EnemyBase enemyBase = user.GetComponent<EnemyBase>();
        FieldManager fieldManager = GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager));
        RoomData room = fieldManager.GetRoomData(enemyBase.RoomID);
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;

        _minPos = room.Position.UpperLeft;
        _maxPos = room.Position.BottomRight;
    }

    public bool Try()
    {
        if (_minPos.x < _player.position.x + 1 && _maxPos.x >= _player.position.x)
        {
            if (_minPos.z < _player.position.z + 1 && _maxPos.z >= _player.position.z)
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
