using UnityEngine;

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
        Debug.Log("aa");
        _physicsBase.SetForce(_forceDir, _forcePower);
    }
}
