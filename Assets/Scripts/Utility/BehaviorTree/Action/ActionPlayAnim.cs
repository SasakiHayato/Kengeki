using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̍s���N���X�BAnimation�̎��s
/// </summary>

public class ActionPlayAnim : IAction
{
    [SerializeField] string _animName;

    CharaBase _charaBase;

    public void SetUp(GameObject user)
    {
        _charaBase = user.GetComponent<CharaBase>();
    }

    public bool Execute()
    {
        _charaBase.Anim.Play(_animName);
        return true;
    }

    public void InitParam()
    {
        
    }
}
