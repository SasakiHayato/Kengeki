using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree‚ÌğŒƒNƒ‰ƒXB‘ÎÛ‚ÌHP‚É‘Î‚·‚é¬”Û
/// </summary>

public class ConditionCheckHP : IConditional
{
    [SerializeField, Range(0, 1)] float _effectParsent;
    CharaBase _charaBase;

    public void SetUp(GameObject user)
    {
        _charaBase = user.GetComponent<CharaBase>();
    }

    public bool Try()
    {
        float effectHp = Mathf.Lerp(0, _charaBase.CharaData.MaxHP, _effectParsent);

        if (_charaBase.CharaData.HP < effectHp) return true;
        else return false;
    }

    public void InitParam()
    {
        
    }
}
