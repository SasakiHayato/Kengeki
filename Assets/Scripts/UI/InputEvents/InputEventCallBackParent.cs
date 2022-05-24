using UnityEngine;

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
