using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] Transform _offestPosition;

    ObjectType _type;
    Collider _collider;

    AttackSetting _attackSetting;

    public Transform OffsetPosition
    {
        get
        {
            if (_offestPosition != null) return _offestPosition;
            else return transform;
        }
    }

    public void SetUp(ObjectType type, AttackSetting setting)
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _collider.enabled = false;

        _type = type;
        _attackSetting = setting;
    }

    public void SetColliderActive(bool active)
    {
        _collider.enabled = active;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharaBase chara = other.GetComponent<CharaBase>();

        if (chara != null && chara.Data.ObjectType != _type)
        {
            IDamage iDamage = other.GetComponent<IDamage>();

            if (iDamage != null) _attackSetting.IsHit(iDamage, other.gameObject);
        }
    }
}
