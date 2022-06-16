/// <summary>
/// ゲームパッドからの入力に対する実行クラス。Itemの実行
/// </summary>

public class InputEventLoadItem : IInputEvents
{
    UIManager _uiManager;

    public void SetUp()
    {
        _uiManager = GameManager.Instance.GetManager<UIManager>(nameof(UIManager));
    }

    public void Execute()
    {
        _uiManager.InputLoadItem();
    }
}
