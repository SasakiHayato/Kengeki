using UnityEngine;

/// <summary>
/// ゲームパッドからの入力に対する実行クラス。Soundの音量調整
/// </summary>

public class InputEventChageSoundValue : IInputEvents
{
    [SerializeField] float _addCount;
    [SerializeField] SoundType _soundType;

    SoundManager _soundManager;

    public void SetUp()
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
    }

    public void Execute()
    {
        switch (_soundType)
        {
            case SoundType.BGM:

                _soundManager.AddBGMVolume(_addCount);
                break;
            case SoundType.SE:

                _soundManager.AddSEVolume(_addCount);
                break;

        }
    }
}
