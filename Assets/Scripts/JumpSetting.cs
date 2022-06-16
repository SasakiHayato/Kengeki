using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jump‚·‚éÛ‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class JumpSetting : MonoBehaviour
{
    [SerializeField] List<Data> _datas;

    [System.Serializable]
    class Data
    {
        public float Speed;
        public float Power;
    }

    int _id;
    float _gravity = PhsicsMasterData.GravityCoefficient * -1;
    float _timer;
    bool _isJump = false;

    Data _data;

    public int CurrentID => _id;
    public float Power { get; private set; }
    public bool IsSet { get; private set; }

    void FixedUpdate()
    {
        if (!_isJump) return;

        _timer += Time.fixedDeltaTime * _data.Speed;
        Power = _data.Power * _timer - _gravity * _timer * _timer / 2;

        if (Power < 0)
        {
            _isJump = false;
            Power = 0;
        }
    }

    public void Set()
    {
        if (_datas.Count <= _id)
        {
            IsSet = false;
            return;
        }

        _data = _datas[_id];

        _id++;
        _timer = 0;
        _isJump = true;

        IsSet = true;
    }

    public void Init()
    {
        _id = 0;
        _timer = 0;
        _isJump = false;
    }
}
