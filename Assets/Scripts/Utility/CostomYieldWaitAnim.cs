using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostomYieldWaitAnim : CustomYieldInstruction
{
    Animator _anim;
    int _hash = 0;

    bool _tryGet = true;
    bool _isFirst = true;

    public override bool keepWaiting
    {
        get
        {
            if (!_tryGet)
            {
                Debug.Log("Nothing Animator");
                return false;
            }

            if (_isFirst)
            {
                _isFirst = false;
                return false;
            }

            var info = _anim.GetCurrentAnimatorStateInfo(0);
            int currenthash = _anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            return info.normalizedTime < 1 && currenthash == _hash;
        }
    }

    public CostomYieldWaitAnim(Animator anim)
    {
        if (anim == null)
        {
            _tryGet = false;
            return;
        }

        _anim = anim;
        _hash = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        _isFirst = true;
    }

}
