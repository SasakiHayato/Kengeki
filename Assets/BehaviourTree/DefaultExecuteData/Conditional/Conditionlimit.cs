using UnityEngine;
using BehaviourTree.Execute;
using BehaviourTree.Data;

/// <summary>
/// ‰ñ”§ŒÀ‚Ì‚ ‚éAIs“®‚É‚Â‚¯‚éğŒ
/// </summary>
public class Conditionlimit : BehaviourConditional
{
    BehaviourTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        _userData = BehaviourTreeMasterData.Instance.FindUserData(user.GetInstanceID());
    }

    protected override bool Try()
    {
        return _userData.IsLimitCondition();
    }
}
