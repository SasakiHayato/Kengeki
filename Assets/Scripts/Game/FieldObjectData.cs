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

    public void Add(GameObject target, ObjectType type)
    {
        Data data = new Data(target, type);
        _datas.Add(data);
    }

    public Data[] GetData(ObjectType type)
    {
        return _datas.Where(d => d.ObjectType == type).ToArray();
    }

    public class Data
    {
        public Data(GameObject target, ObjectType objectType)
        {
            Target = target;
            ObjectType = objectType;
        }

        public ObjectType ObjectType { get; private set; }
        public GameObject Target { get; private set; }
    }
}
