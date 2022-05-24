using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventChageSoundValue : IInputEvents
{
    [SerializeField] float _addCount;
    [SerializeField] SoundType _soundType;

    SoundManager _soundManager;

    public void SetUp()
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
        Debug.Log(_soundManager);
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