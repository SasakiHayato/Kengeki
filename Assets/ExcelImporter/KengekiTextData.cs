using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class KengekiTextData : ScriptableObject
{
    public List<Data> TextData;

    [System.Serializable]
	public class Data
    {
        public string Path;
        public int ID;
        public string Text;
    }
}
