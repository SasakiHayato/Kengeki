using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

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
