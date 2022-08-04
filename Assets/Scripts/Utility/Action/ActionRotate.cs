using BehaviourTree.Execute;
using UnityEngine;

/// <summary>
/// BehaviorTreeの行動クラス。回転の実行
/// </summary>

public class ActionRotate : BehaviourAction
{
    Transform _user;
    Transform _player;

    protected override void Setup(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    protected override bool Execute()
    {
        Vector3 forward = _player.position - _user.position;
        _user.rotation = Quaternion.LookRotation(forward);

        return true;
    }

    protected override void Initialize()
    {
        
    }
}
