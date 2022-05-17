using UnityEngine;
using BehaviourTree;

public class ActionAttack : IAction
{
    [SerializeField] AttackType _attackType;
    AttackSetting _attackSetting;

    public void SetUp(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    public bool Execute()
    {
        if (_attackSetting.IsNextInput)
        {
            _attackSetting.Request(_attackType);
        }

        
        return true;
    }

    public void InitParam()
    {
        
    }
}
