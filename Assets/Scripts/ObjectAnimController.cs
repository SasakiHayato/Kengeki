using UnityEngine;

/// <summary>
/// Objectのアニメーション管理クラス
/// </summary>

public class ObjectAnimController
{
    bool _existAnim = false;

    Animator _anim;

    const float DurationTime = 0.1f;
    
    public ObjectAnimController(RuntimeAnimatorController runTime, Avatar avatar, GameObject user)
    {
        if (runTime == null) return;
        
        _anim = user.AddComponent<Animator>();
        _anim.runtimeAnimatorController = runTime;
        _anim.avatar = avatar;

        _existAnim = true;
    }

    public void Play(string stateName)
    {
        if (!_existAnim) return;

        _anim.CrossFade(stateName, DurationTime);
    }
}
