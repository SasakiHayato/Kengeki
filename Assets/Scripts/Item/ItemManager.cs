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

    public void SpawnRequest(string path, Transform parent = null)
    {
        ItemDataBase.Data data = _itemDataBase.GetData(path);
        ItemBase itemBase = Instantiate(data.Item);
        itemBase.SetPath(data.Path);
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(ItemManager);
}
