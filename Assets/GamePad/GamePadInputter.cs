
/// <summary>
/// GamePad“ü—Í‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class GamePadInputter : SingletonAttribute<GamePadInputter>
{
    public GamePad Input { get; private set; }

    public override void SetUp()
    {
        Input = new GamePad();
        Input.Enable();
    }

    public static void Despose() => Instance.Input.Dispose();
}
