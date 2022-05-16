using UnityEngine;
using StateMachine;
using System;

public class ShakeCm : State
{
    CmManager _cmManager;

    const float DurationTime = 0.2f;
    const float Magnitude = 0.05f;

    float _timer;

    public override void SetUp(GameObject user)
    {
        _cmManager = user.GetComponent<CmManager>();
    }

    public override void Entry(string beforeStatePath)
    {
        _timer = 0;
    }

    public override void Run()
    {
        float x = UnityEngine.Random.Range(-Magnitude, Magnitude);
        float y = UnityEngine.Random.Range(-Magnitude, Magnitude);
        float z = UnityEngine.Random.Range(-Magnitude, Magnitude);

        _cmManager.CmData.NormalizePosition = new Vector3(x, y, z);

        _timer += Time.deltaTime;
    }

    public override Enum Exit()
    {
        if (_timer > DurationTime) return _cmManager.CmData.SaveState;
        else return CmManager.State.Shake;
    }
}
