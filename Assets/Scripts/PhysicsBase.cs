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
    float _forceTimer;
    bool _isForce;

    Vector3 _forceDir;
    float _forcePower;
    
    CharaBase _charaBase;

    Vector3 _gravity;

    public Vector3 Gravity
    { 
        get
        {
            if (_isForce)
            {
                
                return _gravity;
            }
            else
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

        private set
        {
            _gravity = value;
        }
    }

    public bool IsGround { get; private set; }

    void Start()
    {
        _charaBase = GetComponent<CharaBase>();
        _isForce = false;
    }

    void Update()
    {
        CheckGround();
        ForceUpdate();
    }

    void FixedUpdate()
    {
        _gravityTimer += Time.fixedDeltaTime * PhsicsMasterData.GravityScale;

        if (_isForce) _forceTimer += Time.fixedDeltaTime * PhsicsMasterData.GravityScale;
    }

    void CheckGround()
    {
        Debug.DrawRay(_charaBase.OffsetPosition.position, _rayDirection.normalized, Color.red);

        if (Physics.Raycast(_charaBase.OffsetPosition.position, _rayDirection.normalized, _rayDistance, _hitLayer))
        {
            _gravityTimer = 0;
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }
    }

    void ForceUpdate()
    {
        if (!_isForce) return;

        float g = PhsicsMasterData.GravityCoefficient * -1;
        float v = _forcePower - _forceTimer * g;

        _gravity = _forceDir * v;

        if (v < 0)
        {
            _isForce = false;
            _forcePower = 0;
            _forceDir = Vector3.zero;
            _forceTimer = 0;
        }
    }

    public void SetForce(Vector3 dir, float power)
    {
        _forceDir = dir.normalized;
        _forcePower = power;

        _isForce = true;
    }

    public void InitializeTumer()
    {
        _gravityTimer = 0;
    }
}
