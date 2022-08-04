using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree‚ÌğŒƒNƒ‰ƒXB‘ÎÛ‚ÌHP‚É‘Î‚·‚é¬”Û
/// </summary>

public class ConditionCheckHP : BehaviourConditional
{
    [SerializeField, Range(0, 1)] float _effectParsent;
    CharaBase _charaBase;

    protected override void Setup(GameObject user)
    {
        _charaBase = user.GetComponent<CharaBase>();
    }

    protected override bool Try()
    {
        float effectHp = Mathf.Lerp(0, _charaBase.CharaData.MaxHP, _effectParsent);

        if (_charaBase.CharaData.HP < effectHp) return true;
        else return false;
    }

    protected override void Initialize()
    {
        
    }
}
