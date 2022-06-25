using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̍s���N���X�B�W�I������ۂ�Pod�̉�]
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
