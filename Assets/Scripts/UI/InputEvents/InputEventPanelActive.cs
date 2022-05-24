using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventPanelActive : IInputEvents
{
    public void SetUp()
    {
        Debug.Log("Set");
    }

    public void Execute()
    {
        Debug.Log("aa");
    }
}
