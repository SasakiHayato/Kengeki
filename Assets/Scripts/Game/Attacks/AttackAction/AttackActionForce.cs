using UnityEngine;

/// <summary>
/// 攻撃時の実行クラス。任意の方向に飛ばす
/// </summary>

public class AttackActionForce : IAttackAction
{
    [SerializeField] Vector3 _forceDir;
    [SerializeField] float _forcePower;

    PhysicsBase _physicsBase;

    public void SetUp(GameObject user)
    {
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public void Execute()
    {
        _physicsBase.SetForce(_forceDir, _forcePower);
    }
}
