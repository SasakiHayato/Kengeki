using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Item‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class ItemManager : ManagerBase
{
    [SerializeField] ItemDataBase _itemDataBase;

    List<ItemBase> _fieldItemList;

    void Start()
    {
        GameManager.Instance.AddManager(this);
        _fieldItemList = new List<ItemBase>();
        GamePadInputter.Instance.Input.Player.Entry.performed += context => Get();
    }

    void Get()
    {
        ItemBase itemBase = _fieldItemList.FirstOrDefault(f => f.IsEffect && f.gameObject.activeSelf);
        if (itemBase.Get())
        {
            RemoveItem(itemBase);
        }
    }

    public void RemoveItem(ItemBase itemBase)
    {
        _fieldItemList.Remove(itemBase);
    }

    public override void SetUp()
    {
        if (ItemDirectory.Instance == null)
        {
            ItemDirectory.SetInstance(new ItemDirectory()).SetUp();
        }

        base.SetUp();
    }

    public void SpawnRequest(string path, Transform parent = null)
    {
        ItemDataBase.Data data = _itemDataBase.GetData(path);
        ItemBase itemBase = Instantiate(data.ItemPrefab);
        itemBase.SetInfo(data);

        itemBase.transform.position = parent.position;

        _fieldItemList.Add(itemBase);
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(ItemManager);
}
