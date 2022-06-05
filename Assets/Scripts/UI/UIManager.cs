using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase
{
    [SerializeField] List<GamePadInputEvent> _eventList;
    [SerializeField] ItemViewer _itemViewer;

    void Start()
    {
        GameManager.Instance.AddManager(this);
    }

    public override void SetUp()
    {
        base.SetUp();

        BaseUI.SetInstance(new BaseUI()).SetUp();
        _eventList.ForEach(e => e.SetUp());
    }

    public void UpdateItemInfo(UpdateViewType type)
    {
        _itemViewer.UpdateInfo(type);
    }

    public void InputLoadItem()
    {
        _itemViewer.PickUpLoad();
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(UIManager);
}
