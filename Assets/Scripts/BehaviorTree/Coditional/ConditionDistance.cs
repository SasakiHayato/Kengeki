using UnityEngine;
using BehaviourTree;

public class ConditionDistance : IConditional
{
    [SerializeField] float _effectDistance;

    Transform _user;
    Transform _player;

    public void SetUp(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    public bool Try()
    {
        return _effectDistance > Vector3.Distance(_user.position, _player.position);
    }

    public void InitParam()
    {
        
    }
}
