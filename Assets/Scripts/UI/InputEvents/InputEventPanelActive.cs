using UnityEngine;

/// <summary>
/// ゲームパッドからの入力に対する実行クラス。各UIのActiveを変更
/// </summary>

public class InputEventPanelActive : IInputEvents
{
    [SerializeField] string _pathName;
    [SerializeField] bool _active;

    public void SetUp()
    {
        
    }

    public void Execute()
    {
        BaseUI.Instance.ParentActive(_pathName, _active);
    }
}
