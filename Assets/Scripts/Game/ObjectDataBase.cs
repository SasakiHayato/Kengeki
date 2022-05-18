using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu (fileName = "ObjectDataBase")]
public class ObjectDataBase : ScriptableObject
{
    [SerializeField] List<Data> _datas;

    public Data GetData(string path) => _datas.FirstOrDefault(d => d.Path == path);

    [System.Serializable]
    public class Data
    {
        public GameObject Prefab;
        public string Name;
        public int HP;
        public float Speed;
        public int Power;
        public RuntimeAnimatorController Runtime;
        public Avatar Avatar;
        public ObjectType ObjectType;
        public int ID;
        public string Path;
    }
}
