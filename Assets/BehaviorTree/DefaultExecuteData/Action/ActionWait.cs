using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// w’èŠÔ‘Ò‚½‚¹‚éAIs“®
/// </summary>
public class ActionWait : Action
{
    [SerializeField] float _waitTime;

    float _timer;
    protected override bool Execute()
    {
        _timer += Time.deltaTime;
        return _timer > _waitTime;
    }

    protected override void Initialize()
    {
        _timer = 0;
    }

    protected override void Setup(GameObject user)
    {
        
    }
}
