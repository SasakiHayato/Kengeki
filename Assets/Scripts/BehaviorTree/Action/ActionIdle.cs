using UnityEngine;
using BehaviourTree;

public class ActionIdle : IAction
{
    [SerializeField] string _animName;

    EnemyBase _enemyBase;

    public void SetUp(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
    }

    public bool Execute()
    {
        if (_animName != "") _enemyBase.Anim.Play(_animName);

        return true;
    }

    public void InitParam()
    {
        
    }
}