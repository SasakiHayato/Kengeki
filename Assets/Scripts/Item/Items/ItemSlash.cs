using UnityEngine;

public class ItemSlash : ItemBase
{
    [SerializeField] float _effectTime;

    public override bool Execute()
    {
        GameObject obj = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target;
        obj.GetComponent<Player>().SetOnBerserker(_effectTime);

        return true;
    }
}
