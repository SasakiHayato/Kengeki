using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。攻撃のリクエスト
/// </summary>

public class ActionAttack : BehaviourAction
{
    [SerializeField] AttackType _attackType;
    [SerializeField] bool _attributeID;
    [SerializeField] int _requestID = -1;

    AttackSetting _attackSetting;
    
    protected override void Setup(GameObject user)
    {
        _attackSetting = user.GetComponent<AttackSetting>();
    }

    protected override bool Execute()
    {
        if (_attackSetting.IsNextInput)
        {
            if (_attributeID) _attackSetting.RequestAt(_attackType, _requestID);
            else _attackSetting.Request(_attackType);
        }

        return true;
    }

    protected override void Initialize()
    {
        
    }
}
