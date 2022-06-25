using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading;

/// <summary>
/// çUåÇÇ∑ÇÈÇ∑ÇÈç€ÇÃä«óùÉNÉâÉX
/// </summary>

public class AttackSetting : MonoBehaviour
{
    [SerializeField] CharaBase _user;
    [SerializeField] AttackCollider _targetCollider;
    [SerializeField] List<AttackDataBase> _attackDatas;

    int _id = 0;
    AttackType _saveAttackType;

    public bool IsNextInput { get; private set; }

    AttackDataBase.Data _data;
    SoundManager _soundManager;

    CancellationTokenSource _waitEndAnimSource;
    CancellationTokenSource _waitNextInputSource;
    CancellationTokenSource _waitExecuteActionSource;

    const int Span = 30;

    public void SetUp()
    {
        _attackDatas.ForEach(a => a.GetDatas.ForEach(a =>
        {
            a.Action.AttackAction?.SetUp(_user.gameObject);
            a.Action.HitActions?.ForEach(a => a.SetUp(_user.gameObject));
        }));

        _targetCollider.SetUp(_user.CharaData.ObjectType, IsHit);
        IsNextInput = true;

        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
    }

    public bool Request(AttackType type)
    {
        if (!IsNextInput) return false;
        IsNextInput = false;

        AttackDataBase dataBase = _attackDatas.FirstOrDefault(d => d.AttackType == type);

        TypeCheck(dataBase, type);

        _data = dataBase.GetData(_id);
        _user.Anim.SetAnimEvent(() => ColliderActive(true), _data.IsActiveFrame).Play(_data.AnimName);

        if (_data.SoundName != "")
        {
            _soundManager.Request(SoundType.SE, _data.SoundName);
        }

        WaitEndActive(_data.EndActiveFrame).Forget();
        WaitNextInput(_data.NextInputFrame).Forget();
        WaitExecuteAction(_data.Action.ExecuteFrame).Forget();

        _id++;
        
        return true;
    }

    public bool RequestAt(AttackType type, int id)
    {
        if (!IsNextInput) return false;
        IsNextInput = false;

        AttackDataBase dataBase = _attackDatas.FirstOrDefault(d => d.AttackType == type);

        TypeCheck(dataBase, type);

        _data = dataBase.GetData(id);
        _user.Anim.SetAnimEvent(() => ColliderActive(true), _data.IsActiveFrame).Play(_data.AnimName);

        if (_data.SoundName != "")
        {
            _soundManager.Request(SoundType.SE, _data.SoundName);
        }

        WaitEndActive(_data.EndActiveFrame).Forget();
        WaitNextInput(_data.NextInputFrame).Forget();
        WaitExecuteAction(_data.Action.ExecuteFrame).Forget();

        _id++;

        return true;
    }

    public void Cancel()
    {
        IsNextInput = true;
        _id = 0;
        _waitEndAnimSource?.Cancel();
        _waitNextInputSource?.Cancel();

        ColliderActive(false);
    }

    async UniTask WaitEndActive(int waitFrame)
    {
        _waitEndAnimSource = new CancellationTokenSource();
        CancellationToken token = _waitEndAnimSource.Token;
        await UniTask.Delay(waitFrame * Span, false, PlayerLoopTiming.Update, token);
        ColliderActive(false);
    }

    async UniTask WaitNextInput(int waitFrame)
    {
        _waitNextInputSource = new CancellationTokenSource();
        CancellationToken token = _waitNextInputSource.Token;
        await UniTask.Delay(waitFrame * Span, false, PlayerLoopTiming.Update, token);

        IsNextInput = true;
    }

    async UniTask WaitExecuteAction(int waitFrame)
    {
        _waitExecuteActionSource = new CancellationTokenSource();
        CancellationToken token = _waitExecuteActionSource.Token;
        await UniTask.Delay(waitFrame * Span, false, PlayerLoopTiming.Update, token);
        _data.Action.AttackAction?.Execute();
    }

    void ColliderActive(bool active)
    {
        _targetCollider.SetColliderActive(active);
    }

    public void IsHit(IDamage iDamage, Collider target)
    {
        int power = _data.Power + _user.CharaData.Power;

        if (iDamage.GetDamage(power))
        {
            _data.Action.HitActions?.ForEach(a => a.Execute(target));
            BaseUI.Instance.CallBack("GameUI", "Damage", new object[] { power, target.transform });
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
