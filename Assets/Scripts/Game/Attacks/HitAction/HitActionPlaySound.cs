using UnityEngine;

/// <summary>
/// 攻撃ヒット時の実行クラス。音を鳴らす
/// </summary>

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
