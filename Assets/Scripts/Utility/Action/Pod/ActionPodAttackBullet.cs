using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの行動クラス。Podの攻撃
/// </summary>

public class ActionPodAttackBullet : Action
{
    [SerializeField] float _coolTime;
    [SerializeField] float _bulletSpeed;

    float _timer;
    BulletManager _bulletManager;
    Pod _pod;

    Vector3 _beforePos;

    protected override void Setup(GameObject user)
    {
        _timer = 0;
        _bulletManager = GameManager.Instance.GetManager<BulletManager>(nameof(BulletManager));
        _pod = user.GetComponent<Pod>();
    }

    protected override bool Execute()
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

    protected override void Initialize()
    {
        _beforePos = Vector3.zero;
        _timer = 0;
    }
}
