using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class PlayerHPSlider : ChildrenUI
{
    Player _player;
    Slider _slider;

    const float DurationTime = 0.2f;

    public override void SetUp()
    {
        _slider = GetComponent<Slider>();

        _player.ObserveEveryValueChanged(x => x.CharaData.HP)
            .TakeUntilDestroy(_player)
            .Subscribe(x => ChangeValue(x));
    }

    void ChangeValue(int val)
    {
        DOTween.To
            (
                () => _slider.value,
                (x) => _slider.value = x,
                val,
                DurationTime
            )
            .SetEase(Ease.Linear);
    }

    public override void CallBack(object[] datas = null)
    {

        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.GetComponent<Player>();


        _slider.maxValue = _player.CharaData.HP;
        _slider.value = _player.CharaData.HP;
    }
}
