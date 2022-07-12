using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// Debug—p‚ÌğŒw’è‚µ‚½Bool‚ğ•Ô‚·
/// </summary>
public class ConditionTestAccess : Conditional
{
    [SerializeField] bool _isAccess;

    protected override void Setup(GameObject user)
    {
        
    }

    protected override bool Try()
    {
        return _isAccess;
    }

    protected override void Initialize()
    {

    }
}
