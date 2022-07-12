using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree�̍s���N���X�B�Ώۂ�Scale�ύX
/// </summary>

public class ActionChangeScale : Action
{
    [SerializeField] GameObject _terget;
    [SerializeField] Vector3 _setScale;

    protected override void Setup(GameObject user)
    {
        
    }

    protected override bool Execute()
    {
        _terget.transform.localScale = _setScale;
        return true;
    }

    protected override void Initialize()
    {
        
    }
}
