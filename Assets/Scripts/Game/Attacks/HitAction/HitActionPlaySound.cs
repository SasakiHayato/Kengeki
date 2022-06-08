using UnityEngine;

public class HitActionPlaySound : IHitAction
{
    [SerializeField] string _soundName;

    SoundManager _soundManager;

    public void SetUp(GameObject user)
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
    }

    public void Execute(Collider collider)
    {
        _soundManager.Request(SoundType.SE, _soundName);
    }
}
