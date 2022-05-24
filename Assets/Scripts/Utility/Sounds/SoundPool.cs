using UnityEngine;

public class SoundPool : MonoBehaviour, IPool
{
    public bool Waiting { get; set; }

    SoundManager _soundManager;
    AudioSource _source;

    SoundType _soundType;
    float _volume;

    bool _isSet;

    public void SetUp(Transform parent)
    {
        _source = gameObject.AddComponent<AudioSource>();
        _isSet = false;

        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
    }

    public void SetData(SoundType type, SoundDataBase.Data data)
    {
        _source.volume = data.Volume;
        _source.spatialBlend = data.SpatialBlend;
        _source.clip = data.Clip;

        _soundType = type;

        if (type == SoundType.BGM) _source.loop = true;

        _volume = data.Volume;

        _source.Play();

        _isSet = true;
    }

    public void IsUseSetUp()
    {
        
    }

    public bool Execute()
    {
        if (!_isSet) return true;

        float master = _soundManager.MasterVolume;
        switch (_soundType)
        {
            case SoundType.BGM:

                _source.volume = master * _soundManager.BGMVolume * _volume;
                break;

            case SoundType.SE:

                _source.volume = master * _soundManager.SEVolume * _volume;
                break;
        }

        if (_source.isPlaying) return true;
        else return false;
    }

    public void Delete()
    {
        _isSet = false;
        _source.clip = null;
        _volume = 0;
    }
}
