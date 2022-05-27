using UnityEngine;
using UnityEngine.UI;

public class InputEventCameraInput : IInputEvents
{
    enum CmInputType
    {
        Horisontal,
        Vertical,
    }

    [SerializeField] CmInputType _cmInputType;
    [SerializeField] Text _txt;
    
    CmMasterData.InputType _inputType;

    public void SetUp()
    {
        switch (_cmInputType)
        {
            case CmInputType.Horisontal:

                _inputType = CmMasterData.Horizontal;
                break;
            case CmInputType.Vertical:

                _inputType = CmMasterData.Vertical;
                break;
        }

        _txt.text = _inputType.ToString();
    }

    public void Execute()
    {
        switch (_cmInputType)
        {
            case CmInputType.Horisontal:

                CmMasterData.SetHorizontalInput(_inputType);
                break;
            case CmInputType.Vertical:

                CmMasterData.SetVerticalInput(_inputType);
                break;
        }

        _txt.text = _inputType.ToString();
        Debug.Log($"{_txt.text}");

        if (_inputType == CmMasterData.InputType.Inversion)
        {
            _inputType = CmMasterData.InputType.Normal;
        }
        else
        {
            _inputType = CmMasterData.InputType.Inversion;
        }

        
    }
}
