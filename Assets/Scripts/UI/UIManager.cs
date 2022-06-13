using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase
{
    [SerializeField] List<GamePadInputEvent> _eventList;
    [SerializeField] ItemViewer _itemViewer;
    [SerializeField] LogViewer _logViewer;

    void Start()
    {
        GameManager.Instance.AddManager(this);
    }

    public override void SetUp()
    {
        base.SetUp();

        BaseUI.SetInstance(new BaseUI()).SetUp();
        _eventList.ForEach(e => e.SetUp());

        if (GameManager.Instance.CurrentMapType == MapType.Normal)
        {
            BaseUI.Instance.CallBack("GameUI", "Text", new object[] { GameManager.Instance.TextData.Request("SystemMSG", 1) });
        }
        else
        {
            BaseUI.Instance.CallBack("GameUI", "Text", new object[] { GameManager.Instance.TextData.Request("SystemMSG", 3) });
        }
    }

    public void UpdateItemInfo(UpdateViewType type)
    {
        _itemViewer.UpdateInfo(type);
    }

    public void InputLoadItem()
    {
        _itemViewer.PickUpLoad();
    }

    public void ReqestSetLog(string text)
    {
        _logViewer.SetText(text);
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(UIManager);
}
