using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�BCamera�̓��͂�ύX�����N�G�X�g
/// </summary>

public class InputEventCameraInput : IInputEvents
{
    enum CmInputType
    {
        Horisontal,
        Vertical,
    }

    [SerializeField] CmInputType _cmInputType;
    [SerializeField] Text _txt;
    
    CmInputData.InputType _inputType;

    public void SetUp()
    {
        switch (_cmInputType)
        {
            case CmInputType.Horisontal:

                _inputType = CmInputData.Horizontal;
                break;
            case CmInputType.Vertical:

                _inputType = CmInputData.Vertical;
                break;
        }

        _txt.text = _inputType.ToString();
    }

    public void Execute()
    {
        switch (_cmInputType)
        {
            case CmInputType.Horisontal:

                CmInputData.SetHorizontalInput(_inputType);
                break;
            case CmInputType.Vertical:

                CmInputData.SetVerticalInput(_inputType);
                break;
        }

        _txt.text = _inputType.ToString();
        
        if (_inputType == CmInputData.InputType.Inversion)
        {
            _inputType = CmInputData.InputType.Normal;
        }
        else
        {
            _inputType = CmInputData.InputType.Inversion;
        }
    }
}
