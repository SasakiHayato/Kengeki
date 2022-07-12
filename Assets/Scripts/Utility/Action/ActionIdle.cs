using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Idle
/// </summary>

public class ActionIdle : Action
{
    [SerializeField] string _animName;
    [SerializeField] bool _applyY;

    EnemyBase _enemyBase;

    protected override void Setup(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
    }

    protected override bool Execute()
    {
        if (_animName != "") _enemyBase.Anim.Play(_animName);

        if (!_applyY) _enemyBase.MoveDir = Vector3.zero;
        else _enemyBase.MoveDir = Vector3.up;

        return true;
    }

    protected override void Initialize()
    {
        
    }
}
