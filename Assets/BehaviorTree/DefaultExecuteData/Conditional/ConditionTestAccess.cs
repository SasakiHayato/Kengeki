using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// Debug�p�̏����w�肵��Bool��Ԃ�
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
