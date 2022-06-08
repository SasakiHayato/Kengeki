using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ItemBase : MonoBehaviour
{
    enum InputType
    {
        PlayerInput,
        Touch,
    }

    enum UseTimming
    {
        Now,
        Directory,
    }

    [SerializeField] InputType _inputType;
    [SerializeField] UseTimming _timming;
    [SerializeField] float _effectDist;

    Transform _player;

    protected ItemManager ItemManager { get; private set; }
    public bool IsEffect { get; private set; }

    void Start()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;

        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
        ItemManager = GameManager.Instance.GetManager<ItemManager>(nameof(ItemManager));
    }

    void Update()
    {
        if (_inputType != InputType.PlayerInput || !gameObject.activeSelf) return;

        float dist = Vector3.Distance(_player.position, transform.position);
        if (dist < _effectDist)
        {
            IsEffect = true;
        }
        else
        {
            IsEffect = false;
        }
    }

    public bool Get()
    {
        if (!IsEffect) return false;

        switch (_timming)
        {
            case UseTimming.Now:

                return Execute();
            case UseTimming.Directory:

                ItemDirectory.Instance.Save(this);
                gameObject.SetActive(false);
                return false;
        }

        return false;
    }

    public string Path { get; private set; }
    public string MSG { get; private set; }

    public void SetInfo(ItemDataBase.Data data)
    {
        Path = data.Path;
        MSG = data.MSG;
    }

    public abstract bool Execute();

    private void OnTriggerEnter(Collider other)
    {
        if (_inputType == InputType.PlayerInput) return;

        CharaBase chara = other.GetComponent<CharaBase>();

        if (chara != null && chara.CharaData.ObjectType == ObjectType.GameUser)
        {
            IsEffect = true;
            Get();
        }
    }
}
