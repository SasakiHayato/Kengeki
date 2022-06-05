using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCmAnimation : MonoBehaviour
{
    [SerializeField] Transform _center;
    [SerializeField] float _speed;
    [SerializeField] float _distance;

    float _timer;

    void Update()
    {
        _timer += Time.deltaTime * _speed;

        float rad = _timer * Mathf.Deg2Rad;
        Vector3 setPos = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad)) * _distance + _center.position;
        setPos.y = transform.position.y;
        transform.position = setPos;

        Vector3 forward = (_center.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(forward);
    }
}
