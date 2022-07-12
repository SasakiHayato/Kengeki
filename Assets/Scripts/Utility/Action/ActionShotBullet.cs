using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Bulletのリクエスト
/// </summary>

public class ActionShotBullet : Action
{
    [SerializeField] float _intarvalTime;
    [SerializeField] float _speed;
    [SerializeField] ShotType _shotType;

    CharaBase _charaBase;
    Transform _user;
    Transform _player;
    BulletManager _bulletManager;

    float _timer;
    Vector3 _beforePos;

    protected override void Setup(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
        _charaBase = user.GetComponent<CharaBase>();
    }

    protected override bool Execute()
    {
        if (_bulletManager == null)
        {
            _bulletManager = GameManager.Instance.GetManager<BulletManager>(nameof(BulletManager));
        }

        _timer += Time.deltaTime;

        if (_timer > _intarvalTime)
        {
            Vector3 dir = _bulletManager.SetDir(_shotType, _user, _player);
            Bullet bullet = _bulletManager.ShotRequest(_charaBase.CharaData.ObjectType, dir, _speed, _charaBase.CharaData.Power);

            bullet.transform.position = _user.position;

            return true;
        }

        _beforePos = _player.position;
        return false;
    }

    protected override void Initialize()
    {
        _timer = 0;
    }
}
