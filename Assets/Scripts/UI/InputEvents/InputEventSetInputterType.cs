using UnityEngine;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�BUI�̑��샊�N�G�X�g
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
