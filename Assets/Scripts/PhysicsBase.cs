using UnityEngine;
using System;

public class PhysicsBase : MonoBehaviour
{
    [SerializeField] GravityData _gravityData;

    float _gravityTimer;

    CharaBase _charaBase;

    const float GravityCoefficient = -9.81f;
    
    [Serializable]
    class GravityData
    {
        [SerializeField] bool _useGravity;
        [SerializeField] LayerMask _hitLayer;
        [SerializeField] Vector3 _rayDirection;
        [SerializeField] float _rayDistance;
        [SerializeField] float _scale;

        public bool UseGravity => _useGravity;
        public LayerMask HitLayer => _hitLayer;
        public Vector3 RayDirection => _rayDirection.normalized;
        public float RayDistance => _rayDistance;
        public float Scale => _scale; 
    }

    public Vector3 Gravity
    { 
        get
        {
            if (!_gravityData.UseGravity) return Vector3.zero;
            else return new Vector3(0, GravityCoefficient * _gravityTimer, 0);
        }
    }

    void Start()
    {
        _charaBase = GetComponent<CharaBase>();
    }

    void Update()
    {
        CheckGround();
    }

    void FixedUpdate()
    {
        _gravityTimer += Time.fixedDeltaTime;
    }

    void CheckGround()
    {
        Vector3 dir = _gravityData.RayDirection;
        float dist = _gravityData.RayDistance;

        Debug.DrawRay(_charaBase.OffsetPosition.position, dir * dist, Color.red);

        if (Physics.Raycast(_charaBase.OffsetPosition.position, dir, dist, _gravityData.HitLayer))
        {
            _gravityTimer = 0;
        }
    }
}
