using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    enum UseTimming
    {
        Now,
        Directory,
    }

    [SerializeField] UseTimming _timming;

    protected string Path { get; private set; }

    public void SetPath(string path) => Path = path;

    protected abstract void Execute();

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
                    break;
            }
        }
    }
}
