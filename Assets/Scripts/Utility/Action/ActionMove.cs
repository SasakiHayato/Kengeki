using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Move
/// </summary>

public class ActionMove : Action
{
    enum MoveDirType
    {
        TowardPlayer,
        Back,
        Side,
    }

    [SerializeField] string _animName;
    [SerializeField] MoveDirType _moveDirType;
    [SerializeField] bool _applyY;

    EnemyBase _enemyBase;
    Transform _user;
    Transform _player;

    protected override void Setup(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    protected override bool Execute()
    {
        if (_animName != "") _enemyBase.Anim.Play(_animName);

        Vector3 dir = (_user.position - _player.position).normalized;

        if (!_applyY) dir.y = 0;

        switch (_moveDirType)
        {
            case MoveDirType.TowardPlayer:
                _enemyBase.MoveDir = dir * -1;

                break;
            case MoveDirType.Back:
                _enemyBase.MoveDir = dir;

                break;
            case MoveDirType.Side:
                Vector3 right = _user.right;
                if (!_applyY) right.y = 0;

                _enemyBase.MoveDir = right;

                break;
        }

        return true;
    }

    protected override void Initialize()
    {
        
    }
}
