using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// DebugópÇÃAIèåè
/// </summary>
public class ConditionConsole : BehaviourConditional
{
    protected override void Initialize()
    {
        Debug.Log("ConditionalInit");
    }

    protected override void Setup(GameObject user)
    {
        Debug.Log($"ConditionalSetUp. UserName{user.name}");
    }

    protected override bool Try()
    {
        Debug.Log("Try");
        return true;
    }
}
