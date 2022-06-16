using UnityEngine;

/// <summary>
/// ゲームパッドからの入力に対する実行クラス。Item情報の更新
/// </summary>

public class InputEventUpdateItemInfo : IInputEvents
{
    [SerializeField] UpdateViewType _viewType;

    UIManager _uiManager;

    public void SetUp()
    {
        _uiManager = GameManager.Instance.GetManager<UIManager>(nameof(UIManager));
    }

    public void Execute()
    {
        _uiManager.UpdateItemInfo(_viewType);
    }
}
