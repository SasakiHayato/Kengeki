using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : ManagerBase
{
    [SerializeField] ItemDataBase _itemDataBase;

    void Start()
    {
        GameManager.Instance.AddManager(this);
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
        itemBase.SetPath(data.Path);

        itemBase.transform.position = parent.position;
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(ItemManager);
}
