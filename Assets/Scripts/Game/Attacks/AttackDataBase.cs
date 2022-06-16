using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    Weak,
    Strength,
    Float,
    Counter,
}

public interface IAttackAction
{
    void SetUp(GameObject user);
    void Execute();
}

public interface IHitAction
{
    void SetUp(GameObject user);
    void Execute(Collider collider);
}

/// <summary>
/// 攻撃のデータクラス
/// </summary>

[CreateAssetMenu (fileName = "AttackDataBase")]
public class AttackDataBase : ScriptableObject
{
    [SerializeField] AttackType _type;
    [SerializeField] List<Data> _datas;

    public AttackType AttackType => _type;
    public List<Data> GetDatas => _datas;
    public int Length => _datas.Count;

    public Data GetData(int id) => _datas[id];

    [System.Serializable]
    public class Data
    {
        [SerializeField] string _animName;
        [SerializeField] string _soundName;
        [SerializeField] int _power;
        [SerializeField] int _isActiveFrame;
        [SerializeField] int _endActiveFrame;
        [SerializeField] int _nextInputFrame;
        [SerializeField] AttckEffctType[] _effctTypes;
        [SerializeField] ActionData _actionData;

        public string AnimName => _animName;
        public string SoundName => _soundName;
        public int Power => _power;
        public int IsActiveFrame => _isActiveFrame;
        public int EndActiveFrame => _endActiveFrame;
        public int NextInputFrame => _nextInputFrame;
        public AttckEffctType[] EffctTypes => _effctTypes;
        public ActionData Action => _actionData;

        [System.Serializable]
        public class ActionData
        {
            [SerializeReference, SubclassSelector] List<IHitAction> _hitActions;
            [SerializeReference, SubclassSelector] IAttackAction _attackAction;
            [SerializeField] int _executeFrame;

            public List<IHitAction> HitActions => _hitActions;
            public IAttackAction AttackAction => _attackAction;
            public int ExecuteFrame => _executeFrame;
        }
    }
}
