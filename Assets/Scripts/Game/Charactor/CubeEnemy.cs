using UnityEngine;

public class CubeEnemy : EnemyBase, IDamage
{
    AttackSetting _attackSetting;

    protected override void SetUp()
    {
        base.SetUp();

        _attackSetting = GetComponent<AttackSetting>();
        _attackSetting?.SetUp();
    }

    void Update()
    {
        TreeManager.TreeUpdate();
        
        RB.velocity = MoveDir * CharaData.Speed;
    }

    public bool GetDamage(int damage)
    {
        DestoryRequest();
        return true;
    }

    protected override void DestoryRequest()
    {
        _attackSetting?.Cancel();
        Anim.Cancel();

        base.DestoryRequest();
    }
}
