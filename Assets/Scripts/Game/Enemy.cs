using UnityEngine;

public class Enemy : CharaBase, IDamage
{
    protected override void SetUp()
    {
        base.SetUp();


    }

    public void GetDamage(int damage)
    {
        Debug.Log("Damage");
    }
}
