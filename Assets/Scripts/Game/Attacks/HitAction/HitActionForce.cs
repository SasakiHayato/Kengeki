using UnityEngine;

/// <summary>
/// 攻撃ヒット時の実行クラス。任意の方向に飛ばす
/// </summary>

public class HitActionForce : IHitAction
{
    enum DirType
    {
        Self,
        Forward,
        Up,
    }

    [SerializeField] DirType _type;
    [SerializeField] Vector3 _dir;
    [SerializeField] float _power;

    Transform _user;

    public void SetUp(GameObject user)
    {
        _user = user.transform;
    }

    public void Execute(Collider collider)
    {
        PhysicsBase physicsBase = collider.GetComponent<PhysicsBase>();
        Vector3 dir = Vector3.zero;
        switch (_type)
        {
            case DirType.Self:

                dir = _dir;
                break;
            case DirType.Forward:

                dir = _user.forward;
                break;
            case DirType.Up:

                dir = _user.up;
                break;
        }

        physicsBase?.SetForce(dir, _power);
    }
}
