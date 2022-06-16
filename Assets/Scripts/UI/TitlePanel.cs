/// <summary>
/// TitleUI‚ÌeƒNƒ‰ƒX
/// </summary>

public class TitlePanel : ParentUI
{
    public override void SetUp()
    {
        if (GameState.Title == GameManager.Instance.CurrentGameState)
        {
            base.SetUp();
        }
    }

    public override void CallBack(object[] datas)
    {
        if (GamePadInputter.Instance == null) return;

        GamePadInputter.Instance.RequestGamePadEvents(InputEventsType.Title);
    }
}
