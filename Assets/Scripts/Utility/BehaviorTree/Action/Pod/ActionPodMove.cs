using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。Podの移動
/// </summary>

public class ActionPodMove : IAction
{
    Pod _pod;
    Transform _player;

    public void SetUp(GameObject user)
    {
        _pod = user.GetComponent<Pod>();
        GameObject obj = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target;
        _player = obj.GetComponent<Player>().OffsetPosition;
    }

    public bool Execute()
    {
        _pod.MoveDir = (_player.position - _pod.transform.position).normalized;
        
        return true;   
    }

    public void InitParam()
    {
        
    }
}
