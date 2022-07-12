using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTree‚ÌğŒƒNƒ‰ƒXBPlayer‚Æ‚Ì‹——£‚É‘Î‚·‚é¬”Û
/// </summary>

public class ConditionDistance : Conditional
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

    protected override void Setup(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    protected override bool Try()
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

    protected override void Initialize()
    {
        
    }
}
