using UnityEngine;
using System;

/// <summary>
/// çUåÇîªíËÇÃÇ†ÇÈObjectÇÃä«óùÉNÉâÉX
/// </summary>

[RequireComponent(typeof(Rigidbody))] 
public class AttackCollider : MonoBehaviour
{
    ObjectType _type;
    Rigidbody _rb;
    Collider _collider;

    Action<IDamage, Collider> _hitAction;
    
    bool _isHit = false;

    public void SetUp(ObjectType type, Action<IDamage, Collider> action)
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;
        _rb.freezeRotation = true;

        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _collider.enabled = false;

        _type = type;
        _hitAction= action;
    }

    public void SetColliderActive(bool active)
    {
        _collider.enabled = active;
        _isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        CharaBase chara = other.GetComponent<CharaBase>();

        if (chara != null && chara.CharaData.ObjectType != _type)
        {
            IDamage iDamage = other.GetComponent<IDamage>();
            _isHit = true;

            if (iDamage != null) _hitAction.Invoke(iDamage, other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isHit) return;

        CharaBase chara = other.GetComponent<CharaBase>();
        
        if (chara != null && chara.CharaData.ObjectType != _type)
        {
            IDamage iDamage = other.GetComponent<IDamage>();
            _isHit = true;

            if (iDamage != null) _hitAction.Invoke(iDamage, other);
        }
    }
}
