using UnityEngine;

public class ParticalUser : MonoBehaviour, IPool
{
    ParticleSystem _particleSystem;

    bool _isExecution = false;

    public bool Waiting { get; set ; }

    public void SetUp(Transform parent)
    {
        _particleSystem = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule particlal = _particleSystem.main;
        particlal.stopAction = ParticleSystemStopAction.Callback;

        gameObject.SetActive(false);
    }

    public void IsUseSetUp()
    {
        gameObject.SetActive(true);
        _particleSystem.Play();
        _isExecution = true;
    }

    void OnParticleSystemStopped()
    {
        _isExecution = false;
    }

    public bool Execute()
    {
        return _isExecution;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }
}
