using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    int _buttonCount;

    class ButtonData
    {
        public int ID;
        public string ItemPath;
        public Text Text;
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
        int id = 0;

        for (int index = first; index < end; index++)
        {
            ButtonData data = _buttonDatas.FirstOrDefault(b => b.ID == id);
            ItemDirectory.DirectoryData directory = ItemDirectory.Instance.GetDirectory(index);

            if (directory == null)
            {
                data.Text.text = "Nodata";
            }
            else
            {
                data.Text.text = directory.Path;
            }

            id++;
        }
    }

    void PickUp()
    {
        foreach (ButtonData data in _buttonDatas)
        {
            if (data.ID == GamePadInputter.Instance.SelectID)
            {
                _msgText.text = "";
            }
        }

       
    }
}
