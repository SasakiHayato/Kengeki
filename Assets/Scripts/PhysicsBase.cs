using UnityEngine;

static public class PhsicsMasterData
{
    static public float GravityCoefficient => -9.81f;
    static public float GravityScale = 2;
}

public class PhysicsBase : MonoBehaviour
{
    [SerializeField] bool _useGravity;
    [SerializeField] LayerMask _hitLayer;
    [SerializeField] Vector3 _rayDirection;
    [SerializeField] float _rayDistance;

    float _gravityTimer;
    
    CharaBase _charaBase;

    public Vector3 Gravity
    { 
        get
        {
            if (!_useGravity) return Vector3.zero;
            else
            {
                float gravity = PhsicsMasterData.GravityCoefficient * _gravityTimer;
               
                if (gravity == 0) gravity = -1;

                return new Vector3(1, gravity, 1);
            }
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
        _gravityTimer += Time.fixedDeltaTime * PhsicsMasterData.GravityScale;
    }

    void CheckGround()
    {
        Vector3 dir = _charaBase.OffsetPosition.position + _rayDirection;
        
        if (Physics.Raycast(_charaBase.OffsetPosition.position, dir, _rayDistance, _hitLayer))
        {
            _gravityTimer = 0;
        }
    }
}
