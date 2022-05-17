using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ConditionEmpty : IConditional
{
    public void SetUp(GameObject user)
    {

    }

    public bool Try()
    {
        return true;
    }

    public void InitParam()
    {
        
    }
}
