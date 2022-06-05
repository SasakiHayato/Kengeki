using UnityEngine;
using SingletonAttribute;
using Cysharp.Threading.Tasks;
using System;

public enum AttckEffctType
{
    HitPartical,
    HitStop,
    CmShake,
    Noise,
}

public enum ParticalType
{
    Hit,
    Dead,
    Warning,
    Collect,
    Impulse,
}

public class Effects : SingletonAttribute<Effects>
{
    AttackEffect _attackEffect;

    ObjectPool<ParticleUser> _hitParticalPool;
    ObjectPool<ParticleUser> _deadParticalPool;
    ObjectPool<ParticleUser> _warnigParticalPool;
    ObjectPool<ParticleUser> _collectParticlePool;
    ObjectPool<ParticleUser> _impulseParticlePool;

    const float DodgeTime = 0.3f;
    const float DodgeEffectDuration = 0.1f;

    const string ParticleFileName = "Particle/";

    public override void SetUp()
    {
        _attackEffect = new AttackEffect();

        Transform parent = new GameObject("Effect").transform;

        GameObject hitPartical = (GameObject)Resources.Load(ParticleFileName+"HitParticle");
        _hitParticalPool = new ObjectPool<ParticleUser>(hitPartical.GetComponent<ParticleUser>(), parent, 5);

        GameObject deadPartical = (GameObject)Resources.Load(ParticleFileName+"DeadParticle");
        _deadParticalPool = new ObjectPool<ParticleUser>(deadPartical.GetComponent<ParticleUser>(), parent, 5);

        GameObject warning = (GameObject)Resources.Load(ParticleFileName + "WarnigParticle");
        _warnigParticalPool = new ObjectPool<ParticleUser>(warning.GetComponent<ParticleUser>(), parent, 5);

        GameObject collect = (GameObject)Resources.Load(ParticleFileName + "CollectParticle");
        _collectParticlePool = new ObjectPool<ParticleUser>(collect.GetComponent<ParticleUser>(), parent, 5);

        GameObject impluse = (GameObject)Resources.Load(ParticleFileName + "ImpulseParticle");
        _impulseParticlePool = new ObjectPool<ParticleUser>(impluse.GetComponent<ParticleUser>(), parent, 5);
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
                case AttckEffctType.Noise: RequestNoiseEffect(); 
                    break;
            }
        }
    }

    public ParticleUser RequestParticleEffect(ParticalType type, Transform user = null)
    {
        ParticleUser particle = null;

        switch (type)
        {
            case ParticalType.Hit:
                particle = _hitParticalPool.Use();
                break;
            case ParticalType.Dead:
                particle = _deadParticalPool.Use();
                break;
            case ParticalType.Warning:
                particle = _warnigParticalPool.Use();
                break;
            case ParticalType.Collect:
                particle = _collectParticlePool.Use();
                break;
            case ParticalType.Impulse:
                particle = _impulseParticlePool.Use();
                break;
        }

        if (user != null)
        {
            particle.transform.position = user.position;
        }

        return particle;
    }

    public void RequestDodgeEffect()
    {
        GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager)).Request(SoundType.SE, "SrowMotion");

        CmManager cm = GameManager.Instance.GetManager<CmManager>(nameof(CmManager));
        cm.RadialBlur(0.5f, DodgeEffectDuration);
        cm.GrayScale(1, DodgeEffectDuration);

        Time.timeScale = 0.5f;
        WaitDodgeTime(cm).Forget();
    }

    public void RequestNoiseEffect()
    {
        CmManager cm = GameManager.Instance.GetManager<CmManager>(nameof(CmManager));
        cm.Noise(0.1f, 0.1f);
    }

    async UniTask WaitDodgeTime(CmManager cm)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(DodgeTime));
        cm.RadialBlur(0, DodgeEffectDuration);
        cm.GrayScale(0, DodgeEffectDuration);
        Time.timeScale = 1;
    }

    class AttackEffect
    {
        const float DurationTime = 0.01f;

        public void HiPartical(Transform user)
        {
            Instance.RequestParticleEffect(ParticalType.Hit, user);
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
