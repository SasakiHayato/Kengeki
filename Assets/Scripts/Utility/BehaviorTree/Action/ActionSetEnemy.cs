using UnityEngine;
using System.Collections.Generic;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。Enemyの生成
/// </summary>

public class ActionSetEnemy : IAction
{
    [SerializeField] List<EnemyPath> _enemyPaths;

    EnemyBase _enemyBase;
    FieldManager _fieldManager;

    public void SetUp(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
        _fieldManager = GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager));
    }

    public bool Execute()
    {
        _enemyPaths.ForEach(e => _fieldManager.AtInstanceEnemy(_enemyBase.RoomID, e));
        return true;
    }

    public void InitParam()
    {
        
    }
}
