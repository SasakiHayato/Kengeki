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
        StateManager.Run();

        Vector3 move = Vector3.Scale(MoveDir, PhysicsBase.Gravity);
        
        RB.velocity = move * CharaData.Speed;
    }

    public bool GetDamage(int damage)
    {
        //DestoryRequest();
        return true;
    }

    protected override void DestoryRequest()
    {
        _attackSetting?.Cancel();
        Anim.Cancel();

        base.DestoryRequest();
    }
}
