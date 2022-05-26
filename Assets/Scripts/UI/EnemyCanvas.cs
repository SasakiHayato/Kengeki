using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] EnemyBase _user;
    [SerializeField] Slider _slider;

    public void SetUp()
    {
        _slider.maxValue = _user.CharaData.HP;

        _user.ObserveEveryValueChanged(u => u.CharaData.HP)
            .TakeUntilDestroy(_user)
            .Subscribe(_ => _slider.value = _user.CharaData.HP);
    }
}
