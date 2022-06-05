using UnityEngine;
using BehaviourTree;

public class ActionShotBullet : IAction
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

    public void SetUp(GameObject user)
    {
        _user = user.transform;
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
        _charaBase = user.GetComponent<CharaBase>();
    }

    public bool Execute()
    {
        if (_bulletManager == null)
        {
            _bulletManager = GameManager.Instance.GetManager<BulletManager>(nameof(BulletManager));
        }

        _timer += Time.deltaTime;

        if (_timer > _intarvalTime)
        {
            Vector3 dir = _bulletManager.SetDir(_shotType, _user, _player, _beforePos, _speed);
            Bullet bullet = _bulletManager.ShotRequest(_charaBase.CharaData.ObjectType, dir, _speed, _charaBase.CharaData.Power);

            bullet.transform.position = _user.position;

            return true;
        }

        _beforePos = _player.position;
        return false;
    }

    public void InitParam()
    {
        _timer = 0;
    }
}
