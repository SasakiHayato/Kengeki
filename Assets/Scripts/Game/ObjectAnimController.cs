using UnityEngine;

/// <summary>
/// Object�̃A�j���[�V�����Ǘ��N���X
/// </summary>

public class ObjectAnimController
{
    bool _hasAnim = false;
   
    Animator _anim;

    const float DurationTime = 0.1f;
    
    public ObjectAnimController(RuntimeAnimatorController runTime, Avatar avatar, GameObject user)
    {
        if (runTime == null) return;
        
        _anim = user.AddComponent<Animator>();
        _anim.runtimeAnimatorController = runTime;

        if (avatar != null) _anim.avatar = avatar;

        _hasAnim = true;
    }

    public void Play(string stateName)
    {
        if (!_hasAnim) return;

        _anim.CrossFade(stateName, DurationTime);
    }
}
