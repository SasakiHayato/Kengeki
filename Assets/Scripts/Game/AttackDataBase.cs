using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Weak,
    Float,
    Counter,
}

[CreateAssetMenu (fileName = "AttackDataBase")]
public class AttackDataBase : ScriptableObject
{
    [SerializeField] AttackType _type;
    [SerializeField] List<Data> _datas;

    public AttackType AttackType => _type;
    public Data GetData(int id) => _datas[id];
    public int Length => _datas.Count;

    [System.Serializable]
    public class Data
    {
        [SerializeField] string _animName;
        [SerializeField] int _power;
        [SerializeField] float _isActiveTime;
        [SerializeField] float _endActiveTime;
        [SerializeField] float _nextInputTime;
        [SerializeField] AttckEffctType[] _effctTypes;

        public string AnimName => _animName;
        public int Power => _power;
        public float IsActiveTime => _isActiveTime;
        public float EndActiveTime => _endActiveTime;
        public float NextInputTime => _nextInputTime;
        public AttckEffctType[] EffctTypes => _effctTypes;
    }
}
