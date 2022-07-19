using BehaviourTree.Execute;
using BehaviourTree.Data;
using UnityEngine;

/// <summary>
/// ConditionLimit���Ă΂ꂽ�̂������������Ƃ�ʒm���s��AI�s��
/// </summary>
public class ActionIsCallConditionLimit : BehaviourAction
{
    BehaviourTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        _userData = BehaviourTreeMasterData.Instance.FindUserData(user.GetInstanceID());
    }

    protected override bool Execute()
    {
        _userData.SetLimitConditionalCount(_userData.LimitConditionalCount - 1);
        return true;
    }
}
