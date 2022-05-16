using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;

public class AttackSetting : MonoBehaviour
{
    [SerializeField] CharaBase _user;
    [SerializeField] AttackCollider _targetCollider;
    [SerializeField] List<AttackDataBase> _attackDatas;

    int _id = 0;
    AttackType _saveAttackType;

    AttackDataBase.Data _data;

    void Start()
    {
        _targetCollider.SetUp(_user.Data.ObjectType, this);
    }

    public void Request(AttackType type)
    {
        AttackDataBase dataBase = _attackDatas.FirstOrDefault(d => d.AttackType == type);

        TypeCheck(dataBase, type);

        _data = dataBase.GetData(_id);
        _user.Anim.SetAnimEvent(() => ColliderActive(true), _data.IsActiveTime).Play(_data.AnimName);
        WaitEndActive(_data.EndActiveTime).Forget();

        _id++;
    }

    async UniTask WaitEndActive(float waitSeconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(waitSeconds));

        ColliderActive(false);
    }

    void ColliderActive(bool active)
    {
        _targetCollider.SetColliderActive(active);
    }

    public void IsHit(IDamage iDamage)
    {
        iDamage.GetDamage(_data.Power);
    }

    void TypeCheck(AttackDataBase dataBase, AttackType type)
    {
        if (dataBase.Length <= _id) _id = 0;

        if (_saveAttackType != type)
        {
            _saveAttackType = type;
            _id = 0;
        }
    }

    public void InitalizeID() => _id = 0;
}
