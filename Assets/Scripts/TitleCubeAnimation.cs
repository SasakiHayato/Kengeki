using UnityEngine;
using DG.Tweening;

/// <summary>
/// Title��Cube�ɑ΂���Animation�N���X
/// </summary>

public class TitleCubeAnimation : MonoBehaviour
{
    [SerializeField] Vector3 _endVal;
    [SerializeField] float _duration;

    void Start()
    {
        transform.DORotate(_endVal, _duration, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }
}
