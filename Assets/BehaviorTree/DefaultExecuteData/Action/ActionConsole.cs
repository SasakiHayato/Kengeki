using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// DebugópÇÃAIçsìÆ
/// </summary>
public class ActionConsole : Action
{
    [SerializeField] string _txt;
    protected override bool Execute()
    {
        Debug.Log($"Execute\n{_txt}");
        return true;
    }

    protected override void Initialize()
    {
        Debug.Log("ActionInit");
    }

    protected override void Setup(GameObject user)
    {
        Debug.Log($"SetUpAction. UserName {user}");
    }
}
