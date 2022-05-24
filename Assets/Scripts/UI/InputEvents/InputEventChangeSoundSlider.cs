using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
