using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTree‚ÌğŒƒNƒ‰ƒXBPlayer‚Æ‚Ì‹——£‚É‘Î‚·‚é¬”Û
/// </summary>

public class ConditionDistance : IConditional
{
    enum CheckType
    {
        In,
        Out,
    }

    [SerializeField] float _effectDistance;
    [SerializeField] CheckType _checkType;

    Transform _user;
    Transform _player;

    public void SetUp(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    public bool Try()
    {
        switch (_checkType)
        {
            case CheckType.In:
                return _effectDistance > Vector3.Distance(_user.position, _player.position);

            case CheckType.Out:
                return _effectDistance < Vector3.Distance(_user.position, _player.position);

            default: return false;
        }
    }

    public void InitParam()
    {
        
    }
}
