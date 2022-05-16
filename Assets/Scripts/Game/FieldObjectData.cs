using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Fieldè„ÇÃObjectä«óùÉNÉâÉX
/// </summary>

public class FieldObjectData 
{
    List<Data> _datas;

    public FieldObjectData()
    {
        _datas = new List<Data>();
    }

    public void Add(GameObject target, ObjectType type, int id)
    {
        Data data = new Data(target, type, id);
        _datas.Add(data);
    }

    public Data[] GetData(ObjectType type)
    {
        return _datas.Where(d => d.ObjectType == type).ToArray();
    }

    public void Remove(ObjectType type, int id)
    {
        Data[] datas = GetData(type);
        Data data = datas.ToList().FirstOrDefault(d => d.ID == id);

        _datas.Remove(data);
    }

    public class Data
    {
        public Data(GameObject target, ObjectType objectType, int id)
        {
            Target = target;
            ObjectType = objectType;
            ID = id;
        }

        public ObjectType ObjectType { get; private set; }
        public GameObject Target { get; private set; }
        public int ID { get; private set; }
    }
}
