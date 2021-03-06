using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using SingletonAttribute;

/// <summary>
/// UI管理のベースクラス
/// </summary>

public class BaseUI : SingletonAttribute<BaseUI>
{
    const string CanvasName = "Canvas";
    List<ParentUI> _uiList;

    public Canvas MasterCanvas { get; private set; }

    public override void SetUp()
    {
        MasterCanvas = GameObject.Find(CanvasName).GetComponent<Canvas>();
        ParentUI[] parents = MasterCanvas.GetComponentsInChildren<ParentUI>();

        int id = 0;
        _uiList = new List<ParentUI>();

        foreach (ParentUI ui in parents)
        {
            ui.ID = id;
            ui.gameObject.GetComponent<Image>().raycastTarget = false;
            ui.SetUp();

            _uiList.Add(ui);

            id++;
        }
    }

    public void CallBackParent(int parentID, object[] data = null)
    {
        _uiList.First(u => u.ID == parentID).CallBack(data);
    }

    public void CallBackParent(string parentPath, object[] data = null)
    {
        _uiList.First(u => u.Path == parentPath).CallBack(data);
    }

    public void CallBack(int parentID, int childrenID, object[] data = null)
    {
        var parent = _uiList.First(u => u.ID == parentID);
        parent.UIList.First(u => u.ID == childrenID).CallBack(data);
    }

    public void CallBack(string parentPath, string childrenPath, object[] data = null)
    {
        var parent = _uiList.First(u => u.Path == parentPath);
        parent.UIList.First(u => u.Path == childrenPath).CallBack(data);
    }

    public void CallBack(int parentID, string childrenPath, object[] data = null)
    {
        var parent = _uiList.First(u => u.ID == parentID);
        parent.UIList.First(u => u.Path == childrenPath).CallBack(data);
    }

    public void CallBack(string parentPath, int childrenID, object[] data = null)
    {
        var parent = _uiList.First(u => u.Path == parentPath);
        parent.UIList.First(u => u.ID == childrenID).CallBack(data);
    }

    public void ParentActive(int id, bool active)
    {
        _uiList.First(u => u.ID == id).Active(active);
    }

    public void ParentActive(string path, bool active)
    {
        _uiList.First(u => u.Path == path).Active(active);
    }
}
