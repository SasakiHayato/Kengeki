using UnityEngine;
using BehaviourTree;

public class ActionMove : IAction
{
    enum MoveDirType
    {
        TowardPlayer,
        Back,
    }

    [SerializeField] string _animName;
    [SerializeField] MoveDirType _moveDirType;

    EnemyBase _enemyBase;
    Transform _user;
    Transform _player;

    public void SetUp(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    public bool Execute()
    {
        if (_animName != "") _enemyBase.Anim.Play(_animName);

        switch (_moveDirType)
        {
            case MoveDirType.TowardPlayer:
                _enemyBase.MoveDir = (_user.position - _player.position) * -1;

                break;
            case MoveDirType.Back:
                _enemyBase.MoveDir = (_user.position - _player.position);

                break;
        }

        return true;
    }

    public void InitParam()
    {
        
    }
}
