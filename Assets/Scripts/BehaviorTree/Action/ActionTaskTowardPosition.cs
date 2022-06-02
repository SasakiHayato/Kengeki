using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ActionTaskTowardPosition : IAction
{
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
        Vector3 min = _roomData.Position.UpperLeft;
        Vector3 max = _roomData.Position.BottomRight;

        float x = Random.Range(min.x, max.x);
        float z = Random.Range(min.z, max.z);

        _setPostion = new Vector3((int)x, _user.position.y, (int)z);
    }

    public void InitParam()
    {
        _isCall = false;
        _setPostion = Vector3.zero;
    }
}
