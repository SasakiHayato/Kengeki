using UnityEngine;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�B�eUI��Active��ύX
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
