using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SoundType
{
    BGM,
    SE,
}

/// <summary>
/// Soundのデータクラス
/// </summary>

[CreateAssetMenu (fileName = "SoundDataBase")]
public class SoundDataBase : ScriptableObject
{
    [SerializeField] SoundType _soundType;
    [SerializeField] List<Data> _datas;

    public SoundType SoundType => _soundType;
    public Data GetData(string path) => _datas.FirstOrDefault(d => d.Path == path);

    [System.Serializable]
    public class Data
    {
        public string Path;
        public AudioClip Clip;
        [Range(0, 1)] public float Volume;
        [Range(0, 1)] public int SpatialBlend;
    }
}
