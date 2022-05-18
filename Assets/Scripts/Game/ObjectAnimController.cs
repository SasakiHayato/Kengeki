using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

/// <summary>
/// Objectのアニメーション管理クラス
/// </summary>

public class ObjectAnimController
{
    bool _hasAnim = false;
    string _currentAnimState;

    CancellationTokenSource _tokenSource = null;
   
    Animator _anim;
    RuntimeAnimatorController _runtime;

    public bool EndCurrentAnimNormalizeTime { get; private set; }

    const float DurationTime = 0.1f;
    
    public ObjectAnimController(RuntimeAnimatorController runTime, Avatar avatar, GameObject user)
    {
        if (runTime == null) return;
        
        _anim = user.AddComponent<Animator>();
        _anim.runtimeAnimatorController = runTime;
        _runtime = runTime;

        _currentAnimState = _anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (avatar != null) _anim.avatar = avatar;

        _hasAnim = true;
    }

    public ObjectAnimController Play(string stateName)
    {
        if (!_hasAnim) return this;
        EndCurrentAnimNormalizeTime = false;

        if (stateName == _currentAnimState) return this;
        else _currentAnimState = stateName;

        AnimationClip clip = _runtime.animationClips.FirstOrDefault(a => a.name == stateName);
        if (!clip.isLooping) WaitAnimNormalizeTime(SetToken()).Forget();

        _anim.CrossFade(stateName, DurationTime);

        return this;
    }

    public ObjectAnimController SetAnimEvent(Action action, int executeFrame = 0)
    {
        WaitAnimEvent(action, executeFrame).Forget();
        return this;
    }

    public void Cancel()
    {
        _tokenSource?.Cancel();
    }

    CancellationToken SetToken()
    {
        if (_tokenSource != null)
        {
            _tokenSource.Cancel();
        }

        _tokenSource = new CancellationTokenSource();
        CancellationToken token = _tokenSource.Token;

        return token;
    }

    async UniTask WaitAnimNormalizeTime(CancellationToken token)
    {
        await UniTask.DelayFrame(1, PlayerLoopTiming.Update, token);
        await UniTask.Delay(TimeSpan.FromSeconds(DurationTime));
        await UniTask.WaitUntil(() => _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1, PlayerLoopTiming.Update, token);

        EndCurrentAnimNormalizeTime = true;
    }

    async UniTask WaitAnimEvent(Action action, int waitFrame)
    {
        await UniTask.Delay(waitFrame * 30);   
        action.Invoke();
    }
}
