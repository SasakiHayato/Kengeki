using UnityEngine;

public class CubeEnemy : EnemyBase, IDamage
{
    AttackSetting _attackSetting;

    protected override void SetUp()
    {
        base.SetUp();

        _attackSetting = GetComponent<AttackSetting>();
        _attackSetting.SetUp();
    }

    void Update()
    {
        TreeManager.TreeUpdate();
        
        CharacterController.Move(MoveDir * CharaData.Speed * Time.deltaTime);
    }

    public bool GetDamage(int damage)
    {
        DestoryRequest();
        return true;
    }
}
