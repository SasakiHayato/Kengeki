using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。対象のScale変更
/// </summary>

public class ActionChangeScale : IAction
{
    [SerializeField] GameObject _terget;
    [SerializeField] Vector3 _setScale;

    public void SetUp(GameObject user)
    {
        
    }

    public bool Execute()
    {
        _terget.transform.localScale = _setScale;
        return true;
    }

    public void InitParam()
    {
        
    }
}
