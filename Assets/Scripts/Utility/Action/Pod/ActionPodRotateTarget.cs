using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。標的がいる際のPodの回転
/// </summary>

public class ActionPodRotateTarget : Action
{
    Transform _user;

    protected override void Setup(GameObject user)
    {
        _user = user.GetComponent<Pod>().OffsetPosition;
    }

    protected override bool Execute()
    {
        Vector3 forward = GameManager.Instance.LockonTarget.position - _user.position;
        _user.rotation = Quaternion.LookRotation(forward);

        return true;
    }

    protected override void Initialize()
    {
        
    }
}
