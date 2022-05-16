using UnityEngine;
using SingletonAttribute;
using Cysharp.Threading.Tasks;
using System;

public enum AttckEffctType
{
    HitPartical,
    HitStop,
    CmShake,
}

public enum ParticalType
{
    Hit,
    Dead,
}

public class Effects : SingletonAttribute<Effects>
{
    AttackEffect _attackEffect;

    ObjectPool<ParticalUser> _hitParticalPool;
    ObjectPool<ParticalUser> _deadParticalPool;

    const float DodgeTime = 0.3f;

    public override void SetUp()
    {
        _attackEffect = new AttackEffect();

        GameObject hitPartical = (GameObject)Resources.Load("HitPartical");
        _hitParticalPool = new ObjectPool<ParticalUser>(hitPartical.GetComponent<ParticalUser>(), null, 5);

        GameObject deadPartical = (GameObject)Resources.Load("DeadPartical");
        _deadParticalPool = new ObjectPool<ParticalUser>(deadPartical.GetComponent<ParticalUser>(), null, 5);
    }

    public void RequestAttackEffect(AttckEffctType[] type, Transform user)
    {
        foreach (var item in type)
        {
            switch (item)
            {
                case AttckEffctType.HitPartical: _attackEffect.HiPartical(user);
                    break;
                case AttckEffctType.HitStop: _attackEffect.HitStop();
                    break;
                case AttckEffctType.CmShake: _attackEffect.CmShake();
                    break;
            }
        }
    }

    public void RequestParticalEffect(ParticalType type, Transform user = null)
    {
        ParticalUser partical = null;

        switch (type)
        {
            case ParticalType.Hit:
                partical = _hitParticalPool.Use();
                break;
            case ParticalType.Dead:
                partical = _deadParticalPool.Use();
                break;
        }

        if (user != null)
        {
            partical.transform.position = user.position;
        }
    }

    public void RequestDodgeEffect()
    {
        GameManager.Instance.GetManager<CmManager>(nameof(CmManager)).RadialBlur(1);
        Time.timeScale = 0.5f;
        WaitDodgeTime().Forget();
    }

    async UniTask WaitDodgeTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(DodgeTime));
        GameManager.Instance.GetManager<CmManager>(nameof(CmManager)).RadialBlur(0);
        Time.timeScale = 1;
    }

    class AttackEffect
    {
        const float DurationTime = 0.01f;

        public void HiPartical(Transform user)
        {
            Instance.RequestParticalEffect(ParticalType.Hit, user);
        }

        public void HitStop()
        {
            Time.timeScale = 0.05f;
            WaitDurationTime().Forget();
        }

        async UniTask WaitDurationTime()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DurationTime));
            Time.timeScale = 1;
        }

        public void CmShake()
        {
            GameManager.Instance.GetManager<CmManager>(nameof(CmManager)).Shake();
        }
    }
}
