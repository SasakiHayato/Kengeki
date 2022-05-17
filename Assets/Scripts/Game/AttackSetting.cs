using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class AttackSetting : MonoBehaviour
{
    [SerializeField] CharaBase _user;
    [SerializeField] AttackCollider _targetCollider;
    [SerializeField] List<AttackDataBase> _attackDatas;

    int _id = 0;
    AttackType _saveAttackType;

    public bool IsNextInput { get; private set; }

    AttackDataBase.Data _data;

    CancellationTokenSource _waitEndAnimSource;
    CancellationTokenSource _waitNextInputSource;

    public void SetUp()
    {
        _targetCollider.SetUp(_user.CharaData.ObjectType, this);
        IsNextInput = true;
    }

    public void Request(AttackType type)
    {
        if (!IsNextInput) return;
        IsNextInput = false;

        AttackDataBase dataBase = _attackDatas.FirstOrDefault(d => d.AttackType == type);

        TypeCheck(dataBase, type);

        _data = dataBase.GetData(_id);
        _user.Anim.SetAnimEvent(() => ColliderActive(true), _data.IsActiveTime).Play(_data.AnimName);

        WaitEndActive(_data.EndActiveTime).Forget();
        WaitNextInput(_data.NextInputTime).Forget();

        _id++;
    }

    public void Cancel()
    {
        IsNextInput = true;
        _id = 0;
        _waitEndAnimSource?.Cancel();
        _waitNextInputSource?.Cancel();

        ColliderActive(false);
    }

    async UniTask WaitEndActive(float waitSeconds)
    {
        _waitEndAnimSource = new CancellationTokenSource();
        CancellationToken token = _waitEndAnimSource.Token;
        await UniTask.Delay(TimeSpan.FromSeconds(waitSeconds),false, PlayerLoopTiming.Update, token);

        ColliderActive(false);
    }

    async UniTask WaitNextInput(float waitSecond)
    {
        _waitNextInputSource = new CancellationTokenSource();
        CancellationToken token = _waitNextInputSource.Token;
        await UniTask.Delay(TimeSpan.FromSeconds(waitSecond), false, PlayerLoopTiming.Update, token);

        IsNextInput = true;
    }

    void ColliderActive(bool active)
    {
        _targetCollider.SetColliderActive(active);
    }

    public void IsHit(IDamage iDamage, GameObject target)
    {
        if (iDamage.GetDamage(_data.Power))
        {
            Effects.Instance.RequestAttackEffect(_data.EffctTypes, target.transform);
        }
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
