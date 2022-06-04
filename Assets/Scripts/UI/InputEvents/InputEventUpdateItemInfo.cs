using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventUpdateItemInfo : IInputEvents
{
    [SerializeField] UpdateViewType _viewType;

    UIManager _uiManager;

    public void SetUp()
    {
        _uiManager = GameManager.Instance.GetManager<UIManager>(nameof(UIManager));
    }

    public void Execute()
    {
        _uiManager.UpdateItemInfo(_viewType);
    }
}
