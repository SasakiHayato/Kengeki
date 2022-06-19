using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。PodIdle
/// </summary>

public class ActionPodIdle : IAction
{
    Pod _pod;

    public void SetUp(GameObject user)
    {
        _pod = user.GetComponent<Pod>();
    }

    public bool Execute()
    {
        _pod.MoveDir = Vector3.zero;
        return true;
    }

    public void InitParam()
    {
        
    }
}
