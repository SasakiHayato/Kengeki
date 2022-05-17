
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
    }

    public bool GetDamage(int damage)
    {
        DestoryRequest();
        return true;
    }
}
