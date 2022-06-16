using UnityEngine;

/// <summary>
/// 攻撃時の実行クラス。対象に向かって瞬間移動する
/// </summary>

public class AttackActionTeleportTargetEnemy : IAttackAction
{
    enum OffestPostion
    {
        Toward,
        Back,

        None,
    }

    [SerializeField] OffestPostion _offestPostion;
    [SerializeField] float _setDistance;
    Transform _user;

    public void SetUp(GameObject user)
    {
        _user = user.transform;
    }

    public void Execute()
    {
        Vector3 offsetPos = Vector3.zero;
        Transform target = GameManager.Instance.LockonTarget;

        switch (_offestPostion)
        {
            case OffestPostion.Toward:

                offsetPos = target.forward;
                break;

            case OffestPostion.Back:

                offsetPos = target.forward * -1;
                break;
        }

        _user.position = target.position + (offsetPos * _setDistance);
        _user.rotation = Rotate(target);
    }

    Quaternion Rotate(Transform target)
    {
        Vector3 forward = (_user.position - target.position).normalized;
        forward.y = 0;
        return Quaternion.LookRotation(forward);
    }
}
