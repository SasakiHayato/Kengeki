using UnityEngine;

public class CubeEnemy : CharaBase, IDamage
{
    Transform _player;
    AttackSetting _attackSetting;

    bool _callBack = false;

    protected override void SetUp()
    {
        base.SetUp();

        _player = FindObjectOfType<Player>().transform;
        _attackSetting = GetComponent<AttackSetting>();
        _attackSetting.SetUp();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, _player.position);

        if (dist < 5 && !_callBack)
        {
            _callBack = true;
            _attackSetting.Request(AttackType.Weak);
        }
    }

    public bool GetDamage(int damage)
    {
        DestoryRequest();
        return true;
    }
}
