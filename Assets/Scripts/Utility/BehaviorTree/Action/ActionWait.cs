using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̍s���N���X�B�҂�
/// </summary>

public class ActionWait : IAction
{
    [SerializeField] float _waitTime;

    float _timer;

    public void SetUp(GameObject user)
    {
        
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;
        return _timer > _waitTime;
    }

    public void InitParam()
    {
        _timer = 0;
    }
}
