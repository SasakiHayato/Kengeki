using UnityEngine;

/// <summary>
/// BossÇÃä«óùÉNÉâÉX
/// </summary>

public class BossEnemy : EnemyBase, IDamage
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
        StateManager.Run();

        Vector3 move = Vector3.Scale(MoveDir, PhysicsBase.Gravity);

        if  (PhysicsBase.IsForce) RB.velocity = move;
        else RB.velocity = move * CharaData.Speed;
    }

    public bool GetDamage(int damage)
    {
        _attackSetting?.Cancel();

        int hp = CharaData.HP - damage;
        CharaData.UpdateHP(hp);

        if (CharaData.HP <= 0) DestoryRequest();

        return true;
    }

    protected override void DestoryRequest()
    {
        _attackSetting?.Cancel();
        Anim.Cancel();

        base.DestoryRequest();
    }
}
