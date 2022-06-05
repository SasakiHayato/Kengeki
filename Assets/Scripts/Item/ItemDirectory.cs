using System.Collections.Generic;
using System.Linq;
using SingletonAttribute;
using System;

public class ItemDirectory : SingletonAttribute<ItemDirectory>
{
    public class DirectoryData
    {
        public DirectoryData(string path, string msg)
        {
            Path = path;
            MSG = msg;

            _itemList = new List<ItemBase>();
        }

        public string Path { get; private set; }
        public string MSG { get; private set; }
        public int ItemCount => _itemList.Count;
        List<ItemBase> _itemList;

        public void Add(ItemBase item)
        {
            _itemList.Add(item);
        }

        public void Load()
        {
            _itemList.First().Execute();

            DeleteData();
        }

        void DeleteData()
        {
            _itemList.Remove(_itemList.First());

            if (_itemList.Count <= 0)
            {
                Instance.DeleteDirectory(Path);
            }
        }
    }

    List<DirectoryData> _directoryList;

    public override void SetUp()
    {
        _directoryList = new List<DirectoryData>();
    }

    public void Save(ItemBase item)
    {
        DirectoryData data;

        if (_directoryList.Count <= 0)
        {
            data = CreateDirectory(item);
        }
        else
        {
            data = FindDirectory(item.Path);

            if (data == null)
            {
                data = CreateDirectory(item);
            }
        }

        data.Add(item);
    }

    DirectoryData CreateDirectory(ItemBase item)
    {
        DirectoryData data = new DirectoryData(item.Path, item.MSG);
        _directoryList.Add(data);

        return data;
    }

    DirectoryData FindDirectory(string path)
    {
        return _directoryList.FirstOrDefault(d => d.Path == path);
    }

    public void Load(string path)
    {
        DirectoryData data = FindDirectory(path);

        if (data != null)
        {
            data.Load();
        }
    }

    public DirectoryData GetDirectory(int id)
    {
        DirectoryData data;
        
        try
        {
            data = _directoryList[id];
        }
        catch (Exception)
        {
            data = null;
        }

        return data;
    }

    protected void DeleteDirectory(string path)
    {
        if (_directoryList.Count <= 0) return;

        DirectoryData data = _directoryList.FirstOrDefault(d => d.Path == path);
        _directoryList.Remove(data);
    }
}
