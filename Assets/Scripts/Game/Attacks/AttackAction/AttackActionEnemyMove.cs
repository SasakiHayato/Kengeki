using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// 攻撃時の実行クラス。対象に向かって移動する
/// </summary>

public class AttackActionEnemyMove : IAttackAction
{
    enum MoveType
    {
        Toward,
        Back,
    }

    [SerializeField] MoveType _type;
    [SerializeField] float _moveTime;
    [SerializeField] bool _applyYPosition;
    
    EnemyBase _enemyBase;
    Transform _user;
    Transform _player;

    Vector3 _dir;
    float _timer;

    public void SetUp(GameObject user)
    {
        _enemyBase = user.GetComponent<EnemyBase>();
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
    }

    public void Execute()
    {
        

        switch (_type)
        {
            case MoveType.Toward:

                _dir = _player.position - _user.position;
                break;
            case MoveType.Back:

                _dir = (_player.position - _user.position) * -1;
                break;
        }

        if (!_applyYPosition) _dir.y = 0;

        Task().Forget();
    }

    async UniTask Task()
    {
        await UniTask.WaitUntil(() => Update());
        _timer = 0;
        _dir = Vector3.zero;
        _enemyBase.MoveDir = Vector3.zero;
    }

    bool Update()
    {
        _timer += Time.deltaTime;

        _enemyBase.MoveDir = _dir.normalized;

        return _timer > _moveTime;
    }
}
