
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
