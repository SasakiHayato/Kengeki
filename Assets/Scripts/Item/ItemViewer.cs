using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public enum UpdateViewType
{
    Next,
    Before,

    None,
}

public class ItemViewer : MonoBehaviour
{
    [SerializeField] GameObject _itemButtonPanel;
    [SerializeField] Text _msgText;

    List<ButtonData> _buttonDatas;
    Action _action;

    int _buttonCount;

    int _saveInfoFirst;
    int _saveInfoEnd;

    class ButtonData
    {
        public int ID;
        public string ItemPath;
        public string MSG;
        public Text Text;
        public Action Execute;
    }

    void Start()
    {
        _buttonDatas = new List<ButtonData>();
        _buttonCount = _itemButtonPanel.transform.childCount;

        for (int index = 0; index < _buttonCount; index++)
        {
            ButtonData data = new ButtonData();
            data.ID = index; 
            data.Text = _itemButtonPanel.transform.GetChild(index).GetComponentInChildren<Text>();

            _buttonDatas.Add(data);
        }
    }

    void Update()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.UI) return;

        PickUp();
    }

    public void UpdateInfo(UpdateViewType type)
    {
        switch (type)
        {
            case UpdateViewType.Next:
                break;
            case UpdateViewType.Before:
                break;
            case UpdateViewType.None:

                SetInfo(0, _buttonCount);
                break;
        }
    }

    void SetInfo(int first, int end)
    {
        _saveInfoFirst = first;
        _saveInfoEnd = end;

        int id = 0;

        for (int index = first; index < end; index++)
        {
            ButtonData data = _buttonDatas.FirstOrDefault(b => b.ID == id);
            ItemDirectory.DirectoryData directory = ItemDirectory.Instance.GetDirectory(index);

            if (directory == null)
            {
                data.Text.text = "Nodata";
                data.MSG = "Nodata";
                data.Execute = null;
            }
            else
            {
                data.Text.text = $"{directory.Path}Å~{directory.ItemCount}";
                data.MSG = directory.MSG;
                data.Execute = directory.Load;
            }

            id++;
        }
    }

    void PickUp()
    {
        if (_buttonDatas.Count <= 0) return;

        foreach (ButtonData data in _buttonDatas)
        {
            if (data.ID == GamePadInputter.Instance.SelectID)
            {
                _msgText.text = data.MSG;
                _action = data.Execute;
            }
        }
    }

    public void PickUpLoad()
    {
        _action?.Invoke();
        SetInfo(_saveInfoFirst, _saveInfoEnd);
    }
}
