using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeCollection : MonoBehaviour
{
    [SerializeField] float _collectionDist;

    Transform _player;
    Collider _collider;
    IDamage _damage;

    void Start()
    {
        _player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.transform;
        _collider = GetComponent<Collider>();

        _damage = _player.GetComponent<IDamage>();
    }

    void Update()
    {
        if (!_collider.enabled) return;

        float dist = Vector3.Distance(_player.position, transform.position);
        if (dist < _collectionDist)
        {
            _damage.GetDamage(0);
        }
    }
}
