using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class ParticleUser : MonoBehaviour, IPool
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] float _delayPlayTime;

    bool _isExecution = false;

    public bool Waiting { get; set ; }

    public void SetUp(Transform parent)
    {
        if (_particleSystem == null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        ParticleSystem.MainModule particlal = _particleSystem.main;
        particlal.stopAction = ParticleSystemStopAction.Callback;

        gameObject.SetActive(false);
    }

    public void IsUseSetUp()
    {
        gameObject.SetActive(true);
        DelayPlay().Forget();
        _isExecution = true;
    }

    async UniTask DelayPlay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_delayPlayTime));
        _particleSystem.Play();
    }

    void OnParticleSystemStopped()
    {
        _isExecution = false;
    }

    public bool Execute()
    {
        if (!_particleSystem.isPlaying)
        {
            _isExecution = false;
        }

        return _isExecution;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }
}
