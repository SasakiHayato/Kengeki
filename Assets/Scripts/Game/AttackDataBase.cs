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
        [SerializeField] int _isActiveFrame;
        [SerializeField] int _endActiveFrame;
        [SerializeField] int _nextInputFrame;
        [SerializeField] AttckEffctType[] _effctTypes;

        public string AnimName => _animName;
        public int Power => _power;
        public int IsActiveFrame => _isActiveFrame;
        public int EndActiveFrame => _endActiveFrame;
        public int NextInputFrame => _nextInputFrame;
        public AttckEffctType[] EffctTypes => _effctTypes;
    }
}
