using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Excelのデータ作成クラス
/// </summary>

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
