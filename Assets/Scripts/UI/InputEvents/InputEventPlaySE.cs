using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventPlaySE : IInputEvents
{
    [SerializeField] string _path;
    SoundManager _soundManager;

    public void SetUp()
    {
        
    }

    public void Execute()
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
        _soundManager.Request(SoundType.SE, _path);
    }
}
