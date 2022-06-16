using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。Idle
/// </summary>

public class ActionIdle : IAction
{
    [SerializeField] string _animName;
    [SerializeField] bool _applyY;

    EnemyBase _enemyBase;

    public void SetUp(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
    }

    public bool Execute()
    {
        if (_animName != "") _enemyBase.Anim.Play(_animName);

        if (!_applyY) _enemyBase.MoveDir = Vector3.zero;
        else _enemyBase.MoveDir = Vector3.up;

        return true;
    }

    public void InitParam()
    {
        
    }
}
