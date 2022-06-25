using UnityEngine;

public enum ShotType
{ 
    Toward,
    Deviation,
}

/// <summary>
/// Bullet�S�̂̊Ǘ��N���X
/// </summary>

public class BulletManager : ManagerBase
{
    [SerializeField] Bullet _bullet;

    ObjectPool<Bullet> _bulletPool;

    void Start()
    {
        GameManager.Instance.AddManager(this);
    }

    public override void SetUp()
    {
        _bulletPool = new ObjectPool<Bullet>(_bullet);
        base.SetUp();
    }

    public Bullet ShotRequest(ObjectType type, Vector3 dir, float speed, int power)
    {
        Bullet bullet = _bulletPool.Use();
        bullet.SetData(type, dir.normalized, speed, power);

        return bullet;
    }

    public Bullet ShotRequest(ObjectPool<Bullet> pool, ObjectType type, Vector3 dir, float speed, int power)
    {
        Bullet bullet = pool.Use();
        bullet.SetData(type, dir.normalized, speed, power);

        return bullet;
    }

    public Vector3 SetDir(ShotType type, Transform user, Transform target)
    {
        Vector3 dir = Vector3.zero;

        switch (type)
        {
            case ShotType.Toward:
                dir = target.position - user.position;
                break;
            case ShotType.Deviation:

                //dir = 
                break;
           
        }

        return dir.normalized;
    }
  
    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(BulletManager);
}
