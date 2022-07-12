using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。PodIdle
/// </summary>

public class ActionPodIdle : Action
{
    Pod _pod;

    protected override void Setup(GameObject user)
    {
        _pod = user.GetComponent<Pod>();
    }

    protected override bool Execute()
    {
        _pod.MoveDir = Vector3.zero;
        return true;
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
