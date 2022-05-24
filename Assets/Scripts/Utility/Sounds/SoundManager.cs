using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour, IManager
{
    [SerializeField, Range(0, 1)] float _masterVol;
    [SerializeField, Range(0, 1)] float _bgmVol;
    [SerializeField, Range(0, 1)] float _seVol;
    [SerializeField] int _createPoolCount;
    [SerializeField] SoundPool _soundPrefab;
    [SerializeField] List<SoundDataBase> _soundDataBases;

    ObjectPool<SoundPool> _soundPool;

    public float MasterVolume => _masterVol;
    public float BGMVolume => _bgmVol;
    public float SEVolume => _seVol;

    void Start()
    {
        GameManager.Instance.AddManager(this);

        _soundPool = new ObjectPool<SoundPool>(_soundPrefab, null, _createPoolCount);
    }

    public void Request(SoundType type, string path)
    {
        SoundDataBase dataBase = _soundDataBases.FirstOrDefault(d => d.SoundType == type);

        SoundPool sound = _soundPool.Use();
        sound.SetData(type, dataBase.GetData(path));
    }

    public void AddBGMVolume(float add)
    {
        _bgmVol += add;
        if (_bgmVol > 1) _bgmVol = 1;
        if (_bgmVol < 0) _bgmVol = 0;
    }

    public void AddSEVolume(float add)
    {
        _seVol += add;
        if (_seVol > 1) _seVol = 1;
        if (_seVol < 0) _seVol = 0;
    }

    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => nameof(SoundManager);
}
