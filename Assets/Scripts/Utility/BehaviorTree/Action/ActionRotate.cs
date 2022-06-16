using BehaviourTree;
using UnityEngine;

/// <summary>
/// BehaviorTree�̍s���N���X�B��]�̎��s
/// </summary>

public class ActionRotate : IAction
{
    Transform _user;
    Transform _player;

    public void SetUp(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    public bool Execute()
    {
        Vector3 forward = _player.position - _user.position;
        _user.rotation = Quaternion.LookRotation(forward);

        return true;
    }

    public void InitParam()
    {
        
    }
}
