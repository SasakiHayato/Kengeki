using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeal : ItemBase
{
    [SerializeField] int _healPower;

    public override bool Execute()
    {
        FieldObjectData.Data data = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0];
        Player player = data.Target.GetComponent<Player>();

        int hp = player.CharaData.HP;

        if (hp >= player.CharaData.MaxHP) return false;

        player.CharaData.UpdateHP(_healPower);

        return true;
    }
}
