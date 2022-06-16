using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�B���ʒ������̂���ɑ΂���Slider�̒l�ύX
/// </summary>

public class InputEventChangeSoundSlider : IInputEvents
{
    [SerializeField] Slider _targetSlider;
    [SerializeField] SoundType _soundType;

    SoundManager _soundManager;

    public void SetUp()
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));

        Execute();
    }

    public void Execute()
    {
        switch (_soundType)
        {
            case SoundType.BGM:
                _targetSlider.value = _soundManager.BGMVolume;
                break;

            case SoundType.SE:
                _targetSlider.value = _soundManager.SEVolume;
                break;
        }
    }
}
