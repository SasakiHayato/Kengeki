using UnityEngine;

public class AttackActionSetSlash : IAttackAction
{
    [SerializeField] Bullet _bullet;
    [SerializeField] float _speed;
    Transform _user;

    CharaData _charaData;
    BulletManager _bulletManager;

    ObjectPool<Bullet> _pool;

    const float MinAngle = -45;
    const float MaxAngle = 45;
    
    public void SetUp(GameObject user)
    {
        _user = user.transform;
        _charaData = user.GetComponent<CharaBase>().CharaData;
        _bulletManager = GameManager.Instance.GetManager<BulletManager>(nameof(BulletManager));

        _pool = new ObjectPool<Bullet>(_bullet, null, 1);
    }

    public void Execute()
    {
        Vector3 dir = Vector3.zero;

        switch (_charaData.ObjectType)
        {
            case ObjectType.GameUser:

                Transform t = GameManager.Instance.LockonTarget;

                if (t == null) dir = _user.forward;
                else dir = _bulletManager.SetDir(ShotType.Toward, _user, t);
                break;
            case ObjectType.Enemy:

                GameObject player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target;
                dir = _bulletManager.SetDir(ShotType.Toward, _user, player.transform);
                break;
        }

        Shot(dir);
    }

    void Shot(Vector3 dir)
    {
        Bullet bullet = _bulletManager.ShotRequest(_pool, _charaData.ObjectType, dir, _speed, _charaData.Power);
        bullet.transform.position = _user.position;

        float angle = Random.Range(MinAngle, MaxAngle);
        
        bullet.transform.rotation = Quaternion.LookRotation(_user.forward);
    }
}
