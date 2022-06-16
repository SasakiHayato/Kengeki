using UnityEngine;

/// <summary>
/// ゲームパッドからの入力に対する実行クラス。各UIの親を呼び出す
/// </summary>

public class InputEventCallBackParent : IInputEvents
{
    [SerializeField] string _path;

    public void SetUp()
    {
        
    }

    public void Execute()
    {
        BaseUI.Instance.CallBackParent(_path);
    }
}
