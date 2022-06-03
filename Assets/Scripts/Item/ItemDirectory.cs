using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingletonAttribute;

public class ItemDirectory : SingletonAttribute<ItemDirectory>
{
    class DirectoryData
    {
        public DirectoryData(string path)
        {
            Path = path;
            _itemList = new List<ItemBase>();
        }

        public string Path { get; private set; }
        List<ItemBase> _itemList;
    }

    List<DirectoryData> _directoryList;

    public override void SetUp()
    {
        _directoryList = new List<DirectoryData>();
    }

    public void Save(ItemBase item)
    {
        if (_directoryList.Count <= 0)
        {
            Add(item);
        }
    }

    void Add(ItemBase item)
    {
        DirectoryData data = new DirectoryData("");
    }

    DirectoryData FindDirectory(ItemBase item)
    {
        

        return null;
    }

    public void Load()
    {

    }
}
