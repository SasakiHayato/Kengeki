using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Animationの実行
/// </summary>

public class ActionPlayAnim : BehaviourAction
{
    [SerializeField] string _animName;

    CharaBase _charaBase;

    protected override void Setup(GameObject user)
    {
        _charaBase = user.GetComponent<CharaBase>();
    }

    protected override bool Execute()
    {
        _charaBase.Anim.Play(_animName);
        return true;
    }

    protected override void Initialize()
    {
        
    }
}
