using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
