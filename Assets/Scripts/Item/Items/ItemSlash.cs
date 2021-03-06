using UnityEngine;

/// <summary>
/// 斬撃をだす際の実行クラス
/// </summary>

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
