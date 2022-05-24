using UnityEngine;

public class OptionPanel : ParentUI
{
    [SerializeField] InputEventsType _inputEventsType;

    public override void SetUp()
    {
        base.SetUp();
        Active(false);
    }

    public override void CallBack(object[] datas)
    {
        if (GamePadInputter.Instance == null) return;

        GamePadInputter.Instance.RequestGamePadEvents(_inputEventsType);
    }
}
