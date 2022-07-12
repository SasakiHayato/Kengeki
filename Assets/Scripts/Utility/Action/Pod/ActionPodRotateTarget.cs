using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree�̍s���N���X�B�W�I������ۂ�Pod�̉�]
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
