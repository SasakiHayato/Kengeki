using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ActionWaitNextAttack : IAction
{
    AttackSetting _attackSetting;

    public void SetUp(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    public bool Execute()
    {
        return _attackSetting.IsNextInput;
    }

    public void InitParam()
    {
        
    }
}
