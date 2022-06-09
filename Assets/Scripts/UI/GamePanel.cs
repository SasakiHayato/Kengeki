
public class GamePanel : ParentUI
{
    public override void SetUp()
    {
        if (GameManager.Instance.CurrentGameState == GameState.InGame)
        {
            base.SetUp();
        }
    }

    public override void CallBack(object[] datas)
    {
        
    }
}
