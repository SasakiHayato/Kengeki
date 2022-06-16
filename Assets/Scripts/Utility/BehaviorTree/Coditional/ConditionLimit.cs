using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BehaviourTree;

/// <summary>
/// BehaviorTree�̏����N���X�B��������̎��s�ɑ΂��鐬��
/// </summary>

public class ConditionLimit : IConditional
{
    [SerializeField] int _limitCount;
    [SerializeField] TreeManager.ConditionalType _type;

    [SerializeReference, SubclassSelector] 
    List<IConditional> _conditions;

    public void SetUp(GameObject user)
    {
        _conditions.ForEach(c => c.SetUp(user));
    }

    public bool Try()
    {
        if (_limitCount <= 0) return false;

        bool check;

        if (_type == TreeManager.ConditionalType.Selector)
        {
            check = _conditions.Any(c => c.Try());
        }
        else
        {
            check = _conditions.All(c => c.Try());
        }

        if (check)
        {
            _limitCount--;
        }
        
        return check;
    }

    public void InitParam()
    {
        
    }
}
