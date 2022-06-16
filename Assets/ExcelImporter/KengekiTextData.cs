using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Excel�̃f�[�^�쐬�N���X
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
