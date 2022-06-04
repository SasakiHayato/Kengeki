using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventExecuteItem : IInputEvents
{
    int _buttonID;
    ItemViewer _itemViewer;

    public void SetUp()
    {
        _itemViewer = Object.FindObjectOfType<ItemViewer>();
    }

    public void Execute()
    {
        
    }
}
