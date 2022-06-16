using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。Roomないの移動
/// </summary>

public class ActionTaskTowardPosition : IAction
{
    enum PositionType
    {
        Center,
        Random,
    }

    [SerializeField] PositionType _type;
    [SerializeField] float _updateSpeed;
    [SerializeField] float _effectDist;

    RoomData _roomData;
    Transform _user;
    EnemyBase _enemyBase;

    Vector3 _setPostion;
    bool _isCall;

    public void SetUp(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
        FieldManager fieldManager = GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager));

        _user = _enemyBase.OffsetPosition;
        _roomData = fieldManager.GetRoomData(_enemyBase.RoomID);
    }

    public bool Execute()
    {
        if (!_isCall)
        {
            _isCall = true;
            SetPosition();
        }

        Vector3 dir = _setPostion - _user.position;
        _enemyBase.MoveDir = dir.normalized;
        
        if (Vector3.Distance(_user.position, _setPostion) <= _effectDist)
        {
            _enemyBase.CharaData.UpdateSpeed(_enemyBase.CharaData.DefaultSpeed);
            return true;
        }
        else
        {
            _enemyBase.CharaData.UpdateSpeed(_enemyBase.CharaData.DefaultSpeed + _updateSpeed);
            return false;
        }
    }

    void SetPosition()
    {
        switch (_type)
        {
            case PositionType.Center:

                _setPostion = SetCenter();
                break;
            case PositionType.Random:

                _setPostion = SetRandom();
                break;
        }
    }

    Vector3 SetCenter()
    {
        Vector3 center = _roomData.Position.Center;

        return new Vector3((int)center.x, _user.position.y, (int)center.z);
    }

    Vector3 SetRandom()
    {
        Vector3 min = _roomData.Position.UpperLeft;
        Vector3 max = _roomData.Position.BottomRight;

        float x = Random.Range(min.x, max.x);
        float z = Random.Range(min.z, max.z);

        return new Vector3((int)x, _user.position.y, (int)z);
    }

    public void InitParam()
    {
        _isCall = false;
        _setPostion = Vector3.zero;
    }
}
