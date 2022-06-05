using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ConditionWaitTime : IConditional
{
    [SerializeField] float _waitTime;

    float _timer;

    public void SetUp(GameObject user)
    {
        
    }

    public bool Try()
    {
        _timer += Time.deltaTime;
        return _timer > _waitTime;
    }

    public void InitParam()
    {
        if (_timer > _waitTime)
        {
            _timer = 0;
        }
    }
}
