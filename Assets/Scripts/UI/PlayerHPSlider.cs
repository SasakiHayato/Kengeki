using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class PlayerHPSlider : ChildrenUI
{
    Slider _slider;

    const float DurationTime = 0.2f;

    public override void SetUp()
    {
        _slider = GetComponent<Slider>();

        Player player = GameManager.Instance.Player.GetComponent<Player>();

        player.ObserveEveryValueChanged(x => x.CharaData.HP)
            .TakeUntilDestroy(player)
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
        Player player = GameManager.Instance.Player.GetComponent<Player>();

        _slider.maxValue = player.CharaData.HP;
        _slider.value = player.CharaData.HP;
    }
}
