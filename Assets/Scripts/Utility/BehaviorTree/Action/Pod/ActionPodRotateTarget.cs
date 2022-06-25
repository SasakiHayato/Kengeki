using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。標的がいる際のPodの回転
/// </summary>

public class ActionPodRotateTarget : IAction
{
    Transform _user;

    public void SetUp(GameObject user)
    {
        _user = user.GetComponent<Pod>().OffsetPosition;
    }

    public bool Execute()
    {
        Vector3 forward = GameManager.Instance.LockonTarget.position - _user.position;
        _user.rotation = Quaternion.LookRotation(forward);

        return true;
    }

    public void InitParam()
    {
        
    }
}
