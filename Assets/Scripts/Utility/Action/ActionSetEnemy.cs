using UnityEngine;
using System.Collections.Generic;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Enemyの生成
/// </summary>

public class ActionSetEnemy : BehaviourAction
{
    [SerializeField] List<EnemyPath> _enemyPaths;

    EnemyBase _enemyBase;
    FieldManager _fieldManager;

    protected override void Setup(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
        _fieldManager = GameManager.Instance.GetManager<FieldManager>(nameof(FieldManager));
    }

    protected override bool Execute()
    {
        _enemyPaths.ForEach(e => _fieldManager.AtInstanceEnemy(_enemyBase.RoomID, e));
        return true;
    }

    protected override void Initialize()
    {
        
    }
}
