using UnityEngine;

/// <summary>
/// ゲームパッドからの入力に対する実行クラス。UIの操作リクエスト
/// </summary>

public class InputEventSetInputterType : IInputEvents
{
    [SerializeField] InputterType _inputterType;

    public void SetUp()
    {
        
    }

    public void Execute()
    {
        GamePadInputter.Instance.SetInputterType(_inputterType);
    }
}
