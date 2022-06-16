using UnityEngine;

/// <summary>
/// ゲームパッドからの入力に対する実行クラス。Soundのリクエスト
/// </summary>

public class InputEventPlaySE : IInputEvents
{
    [SerializeField] string _path;
    SoundManager _soundManager;

    public void SetUp()
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
    }

    public void Execute()
    {
        
        _soundManager.Request(SoundType.SE, _path);
    }
}
