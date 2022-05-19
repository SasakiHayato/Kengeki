using UnityEngine;

public class HitActionForce : IHitAction
{
    [SerializeField] Vector3 _dir;
    [SerializeField] float _power;

    public void SetUp(GameObject user)
    {
        
    }

    public void Execute(Collider collider)
    {
        PhysicsBase physicsBase = collider.GetComponent<PhysicsBase>();
        physicsBase?.SetForce(_dir, _power);
    }
}
