using UnityEngine;
using BehaviourTree;
using BehaviourTree.Execute;
using BehaviourTree.Data;

/// <summary>
/// DebugópÇÃAIçsìÆ
/// </summary>
public class ActionConsole : BehaviourAction
{
    [SerializeField] string _txt;

    BehaviourTreeUserData _userData;
    protected override void Setup(GameObject user)
    {
        BehaviourTreeUser treeUser = user.GetComponent<BehaviourTreeUser>();
        _userData = BehaviourTreeMasterData.Instance.FindUserData(treeUser.UserID);

        Debug.Log($"SetUpAction. UserName {_userData.Path}");
    }

    protected override bool Execute()
    {
        Debug.Log($"Execute\n{_txt}");
        return true;
    }

    protected override void Initialize()
    {
        Debug.Log($"ActionInit. User_{_userData.Path}");
    }
}
