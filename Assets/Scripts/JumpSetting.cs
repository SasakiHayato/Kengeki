using System.Collections.Generic;
using UnityEngine;

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

    public float Power { get; private set; }

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
            _id = 0;
            return;
        }

        _data = _datas[_id];

        _id++;
        _timer = 0;
        _isJump = true;
    }
}
