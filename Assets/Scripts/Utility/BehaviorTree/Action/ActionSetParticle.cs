using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの行動クラス。Particleの生成
/// </summary>

public class ActionSetParticle : IAction
{
    enum NextQueue
    {
        WaitEnd,
        IsNext,
    }

    [SerializeField] ParticalType _particalType;
    [SerializeField] NextQueue _nextQueue;
    Transform _user;
    ParticleUser _particleUser;

    bool _isRequest;

    public void SetUp(GameObject user)
    {
        _user = user.GetComponent<CharaBase>().OffsetPosition;
    }

    public bool Execute()
    {
        if (!_isRequest)
        {
            _isRequest = true;
            _particleUser = Effects.Instance.RequestParticleEffect(_particalType, _user);
        }

        if (_nextQueue == NextQueue.IsNext) return true;
        else
        {
            if (_particleUser.Waiting) return true;
            else return false;
        }
    }

    public void InitParam()
    {
        _isRequest = false;
        _particleUser = null;
    }
}
