using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Podの移動
/// </summary>

public class ActionPodMove : BehaviourAction
{
    Pod _pod;
    Transform _player;

    protected override void Setup(GameObject user)
    {
        _pod = user.GetComponent<Pod>();
        GameObject obj = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target;
        _player = obj.GetComponent<Player>().OffsetPosition;
    }

    protected override bool Execute()
    {
        _pod.MoveDir = (_player.position - _pod.transform.position).normalized;

        return true;
    }

    protected override void Initialize()
    {
        
    }
}
