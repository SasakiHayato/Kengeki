using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "ItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField] List<Data> _datas;

    public Data GetData(string path) => _datas.FirstOrDefault(d => d.Path == path);

    [System.Serializable]
    public class Data
    {
        public string Path;
        public string MSG;
        public ItemBase ItemPrefab;
    }
}
