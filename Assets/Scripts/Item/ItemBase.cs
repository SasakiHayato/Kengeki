using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class ItemBase : MonoBehaviour
{
    enum UseTimming
    {
        Now,
        Directory,
    }

    [SerializeField] UseTimming _timming;

    void Start()
    {
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
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
        CharaBase chara = other.GetComponent<CharaBase>();

        if (chara != null && chara.CharaData.ObjectType == ObjectType.GameUser)
        {
            switch (_timming)
            {
                case UseTimming.Now:

                    Execute();
                    break;
                case UseTimming.Directory:

                    ItemDirectory.Instance.Save(this);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }
}
