using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̍s���N���X�B�Ώۂ�Scale�ύX
/// </summary>

public class ActionChangeScale : IAction
{
    [SerializeField] GameObject _terget;
    [SerializeField] Vector3 _setScale;

    public void SetUp(GameObject user)
    {
        
    }

    public bool Execute()
    {
        _terget.transform.localScale = _setScale;
        return true;
    }

    public void InitParam()
    {
        
    }
}
