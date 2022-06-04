using System.Collections.Generic;
using System.Linq;
using SingletonAttribute;
using System;

public class ItemDirectory : SingletonAttribute<ItemDirectory>
{
    public class DirectoryData
    {
        public DirectoryData(string path)
        {
            Path = path;
            _itemList = new List<ItemBase>();
        }

        public string Path { get; private set; }
        List<ItemBase> _itemList;

        public void Add(ItemBase item)
        {
            _itemList.Add(item);
        }

        public void Load()
        {
            _itemList.First().Execute();
            _itemList.Remove(_itemList.First());
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
            data = CreateDirectory(item.Path);
            data.Add(item);
        }
        else
        {
            data = FindDirectory(item.Path);

            if (data == null)
            {
                data = CreateDirectory(item.Path);
                
            }

            data.Add(item);
        }

        _directoryList.Add(data);
    }

    DirectoryData CreateDirectory(string path)
    {
        return new DirectoryData(path);
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
}
