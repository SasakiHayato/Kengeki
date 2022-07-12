using BehaviourTree.Execute;
using BehaviourTree.Data;
using UnityEngine;

/// <summary>
/// ConditionLimit���Ă΂ꂽ�̂������������Ƃ�ʒm���s��AI�s��
/// </summary>
public class ActionIsCallConditionLimit : Action
{
    BehaviorTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        _userData = BehaviorTreeMasterData.Instance.FindUserData(user.GetInstanceID());
    }

    protected override bool Execute()
    {
        _userData.SetLimitConditionalCount(_userData.LimitConditionalCount - 1);
        return true;
    }

    protected override void Initialize()
    {
        
    }
}
