using UnityEngine;
using StateMachine;
using System;

/// <summary>
/// Camera‚ğ—h‚ç‚·Û‚ÌState
/// </summary>

public class ShakeCm : State
{
    CmManager _cmManager;

    const float DurationTime = 0.2f;
    const float Magnitude = 0.05f;

    float _timer;

    Vector3 _savePos;

    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
    }

    public override void Entry(string beforeStatePath)
    {
        _timer = 0;
        _savePos = _cmManager.CmData.Position;
    }

    public override void Run()
    {
        float x = UnityEngine.Random.Range(-Magnitude, Magnitude);
        float y = UnityEngine.Random.Range(-Magnitude, Magnitude);
        float z = UnityEngine.Random.Range(-Magnitude, Magnitude);

        _cmManager.CmData.Position = new Vector3(_savePos.normalized.x, y, _savePos.normalized.z);

        _timer += Time.deltaTime;
    }

    public override Enum Exit()
    {
        if (_timer > DurationTime)
        {
            Debug.Log(_cmManager.CmData.SaveState.ToString());
            return _cmManager.CmData.SaveState;
        }
        else return CmManager.State.Shake;
    }
}
