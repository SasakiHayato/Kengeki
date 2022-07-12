using UnityEngine;
using BehaviourTree.Execute;
using BehaviourTree.Data;

/// <summary>
/// �񐔐����̂���AI�s���ɂ������
/// </summary>
public class Conditionlimit : Conditional
{
    BehaviorTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        _userData = BehaviorTreeMasterData.Instance.FindUserData(user.GetInstanceID());
    }

    protected override bool Try()
    {
        return _userData.IsLimitCondition();
    }

    protected override void Initialize()
    {
        
    }
}
