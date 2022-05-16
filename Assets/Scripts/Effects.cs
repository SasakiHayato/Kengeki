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
    Destory,
}

public class Effects : SingletonAttribute<Effects>
{
    AttackEffect _attackEffect;

    ObjectPool<ParticalUser> _particalPool;

    const float DodgeTime = 0.3f;

    public override void SetUp()
    {
        _attackEffect = new AttackEffect();

        GameObject partical = (GameObject)Resources.Load("HitPartical");
        _particalPool = new ObjectPool<ParticalUser>(partical.GetComponent<ParticalUser>());
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
                partical = _particalPool.Use();
                break;
            case ParticalType.Destory:
                break;
        }

        if (user != null)
        {
            partical.transform.position = user.position;
        }
    }

    public void RequestDodgeEffect()
    {
        Time.timeScale = 0.5f;
        WaitDodgeTime().Forget();
    }

    async UniTask WaitDodgeTime()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(DodgeTime));
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
