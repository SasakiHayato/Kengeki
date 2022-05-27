using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
