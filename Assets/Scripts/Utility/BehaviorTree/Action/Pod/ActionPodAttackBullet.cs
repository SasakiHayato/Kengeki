using UnityEngine;
using BehaviourTree;

public class ActionPodAttackBullet : IAction
{
    [SerializeField] float _coolTime;
    [SerializeField] float _bulletSpeed;

    float _timer;
    BulletManager _bulletManager;
    Pod _pod;

    Vector3 _beforePos;

    public void SetUp(GameObject user)
    {
        _timer = 0;
        _bulletManager = GameManager.Instance.GetManager<BulletManager>(nameof(BulletManager));
        _pod = user.GetComponent<Pod>();
    }

    public bool Execute()
    {
        if (GameManager.Instance.LockonTarget == null)
        {
            return true;
        }

        _timer += Time.deltaTime;

        if (_timer > _coolTime)
        {
            Deviation deviation = new Deviation();
            Transform t = GameManager.Instance.LockonTarget;
            
            Vector3 dir = deviation.DeviationDir(t.position, _pod.OffsetPosition.position, _beforePos, _bulletSpeed);
            Bullet bullet = _bulletManager.ShotRequest(ObjectType.GameUser, dir, _bulletSpeed, _pod.CharaData.Power);
            bullet.transform.position = _pod.OffsetPosition.position;
            return true;
        }
        else
        {
            _beforePos = GameManager.Instance.LockonTarget.position;
            return false;
        }
    }

    public void InitParam()
    {
        _beforePos = Vector3.zero;
        _timer = 0;
    }
}
