using UnityEngine;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�B�eUI�̐e���Ăяo��
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
