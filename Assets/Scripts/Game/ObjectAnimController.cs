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

        if (avatar != null) _anim.avatar = avatar;

        _hasAnim = true;
    }

    public ObjectAnimController Play(string stateName)
    {
        if (!_hasAnim) return this;
        EndCurrentAnimNormalizeTime = false;
        
        AnimationClip clip = _runtime.animationClips.FirstOrDefault(a => a.name == stateName);

        if (!clip.isLooping) WaitAnimNormalizeTime(SetToken()).Forget();

        _anim.CrossFade(stateName, DurationTime);

        return this;
    }

    public ObjectAnimController SetAnimEvent(Action action, float executeTime = 0)
    {
        WaitAnimEvent(action, executeTime).Forget();
        return this;
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

    async UniTask WaitAnimEvent(Action action, float waitSeconds)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(waitSeconds));
        action.Invoke();
    }
}
