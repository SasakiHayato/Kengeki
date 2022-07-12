using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// Debug用の条件指定したBoolを返す
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
