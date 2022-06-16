using UnityEngine;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�BSound�̃��N�G�X�g
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
