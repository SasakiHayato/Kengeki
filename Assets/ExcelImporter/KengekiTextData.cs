using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class KengekiTextData : ScriptableObject
{
    public List<Data> TextData;

	public class Data
    {
        public string Path;
        public int ID;
        public string Text;
    }
}
