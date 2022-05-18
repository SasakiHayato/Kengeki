using UnityEngine;

public enum ShotType
{ 
    Toward,
}

public class BulletManager : MonoBehaviour, IManager
{
    [SerializeField] Bullet _bullet;

    ObjectPool<Bullet> _bulletPool;

    void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(_bullet);

        GameManager.Instance.AddManager(this);
    }

    public Bullet ShotRequest(ObjectType type, Vector3 dir, float speed, int power)
    {
        Bullet bullet = _bulletPool.Use();
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
           
        }

        return dir.normalized;
    }
  
    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => nameof(BulletManager);
}
