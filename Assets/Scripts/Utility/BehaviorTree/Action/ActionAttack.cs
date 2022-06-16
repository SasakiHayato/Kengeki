using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。攻撃のリクエスト
/// </summary>

public class ActionAttack : IAction
{
    [SerializeField] AttackType _attackType;
    [SerializeField] bool _attributeID;
    [SerializeField] int _requestID = -1;

    AttackSetting _attackSetting;
    
    public void SetUp(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    public bool Execute()
    {
        if (_attackSetting.IsNextInput)
        {
            if (_attributeID) _attackSetting.RequestAt(_attackType, _requestID);
            else _attackSetting.Request(_attackType);
        }

        return true;
    }

    public void InitParam()
    {
        
    }
}
