using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
