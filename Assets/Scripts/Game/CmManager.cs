using UnityEngine;
using StateMachine;
using System.Linq;

public static class CmInputData
{
    public enum InputType
    {
        Normal,
        Inversion,
    }

    public static void SetHorizontalInput(InputType type)
    {
        Horizontal = type;
    }

    public static void SetVerticalInput(InputType type)
    {
        Vertical = type;
    }

    public static InputType Horizontal { get; private set; } = InputType.Normal;
    public static InputType Vertical { get; private set; } = InputType.Normal;

    public static int HorizontalInput
    {
        get
        {
            if (Horizontal == InputType.Normal) return 1;
            else return -1;
        }
    }

    public static int VerticalInput
    {
        get
        {
            if (Vertical == InputType.Normal) return 1;
            else return -1;
        }
    }
}

public class CmManager : ManagerBase
{
    public enum State
    {
        Normal,
        Lockon,
        Transition,
        Shake,
    }

    [SerializeField] Transform _user;
    [SerializeField] Vector3 _offsetPosition;
    [SerializeField] Vector3 _offsetView;
    [SerializeField] float _dist;
    [SerializeField, Range(0, 1.0f)] float _deadInput;
    [SerializeField, Range(0, 1.0f)] float _viewDelay;
    [SerializeField] float _sensitivity;
    [SerializeField] LayerMask _collisionLayer;

    float _viewTimer;
    
    Transform _virtualityCm;

    RadialBlurRender _radialrAttribute;
    GrayScaleRender _grayAttribute;
    NoiseRenderer _noiseAttribute;

    StateManager _state;
    public Data CmData { get; private set; }

    public Transform ViewTarget { get; set; }

    public class Data
    {
        public Data(Transform user, float deadIput, float sensitivity)
        {
            User = user;
            DeadInput = deadIput;
            Sensitivity = sensitivity;
        }

        public Transform User { get; private set; }
        public float DeadInput { get; private set; }
        public float Sensitivity { get; private set; }

        Vector3 _position = Vector3.zero;
        public Vector3 NormalizePosition 
        {
            get => _position.normalized; 
            set { _position = value; }
        }

        public float VirticalRate { get; set; }
        public System.Enum SaveState { get; set; }
    }

    void Start()
    {
        GameManager.Instance.AddManager(this);   
    }

    public override void SetUp()
    {
        _virtualityCm = new GameObject("Virtuliry").transform;

        _radialrAttribute = GetComponent<RadialBlurRender>();
        _grayAttribute = GetComponent<GrayScaleRender>();
        _noiseAttribute = GetComponent<NoiseRenderer>();

        base.SetUp();
    }

    public void SetUser()
    {
        if (_user == null)
        {
            Player player = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target.GetComponent<Player>();
            _user = player.OffsetPosition;
        }
        
        CmData = new Data(_user, _deadInput, _sensitivity);

        CmData.VirticalRate = float.MaxValue;

        Vector3 offestPos = _user.position + _offsetPosition;
        transform.position = offestPos;

        ViewTarget = _user;

        _state = new StateManager(gameObject);
        _state.AddState(new NormalCm(), State.Normal)
            .AddState(new LockonCm(), State.Lockon)
            .AddState(new TransitionCm(), State.Transition)
            .AddState(new ShakeCm(), State.Shake)
            .RunRequest(true, State.Normal);
    }

    void Update()
    {
        if (!IsSetUp || _user == null) return;

        _state.Run();

        View();
        Move();
    }

    void Move()
    {
        Vector3 cmPos = CmData.NormalizePosition * (_dist - Zoom());

        if (cmPos.y > CmData.VirticalRate) cmPos.y = CmData.VirticalRate;

        transform.position = cmPos + _user.position;

        Vector3 virtualCmPos = CmData.NormalizePosition * _dist;
        _virtualityCm.position = virtualCmPos;
    }

    void View()
    {
        _viewTimer += Time.deltaTime / _viewDelay;

        Vector3 dir = ViewTarget.position - transform.position + _offsetView;
        Quaternion forward = Quaternion.LookRotation(dir.normalized);
        Quaternion rotate = Quaternion.Lerp(transform.rotation, forward, _viewTimer);

        Vector3 euler = rotate.eulerAngles;

        transform.rotation = Quaternion.Euler(euler);

        if (rotate == transform.rotation) _viewTimer = 0;
    }

    float Zoom()
    {
        float zoomRate;
        float dist = Vector3.Distance(_virtualityCm.position, _user.position);

        Vector3 dir = _virtualityCm.position;
        dir.y -= 0.5f;
        
        RaycastHit hit;
        if (Physics.Raycast(_user.position, dir, out hit, dist, _collisionLayer))
        {
            zoomRate = _dist - hit.distance;
        }
        else
        {
            zoomRate = 0;
        }

        return zoomRate;
    }

    public void Shake()
    {
        CmData.SaveState = _state.CurrentStatePath;
        _state.ChangeState(State.Shake);
    }

    public void SetSensivity(float addVal) => _sensitivity += addVal;

    public void RadialBlur(float strength, float duration = 1)
    {
        _radialrAttribute.SetStrength(strength, duration);
    }

    public void GrayScale(float strangth, float duration = 1)
    {
        _grayAttribute.SetStrength(strangth, duration);
    }

    public void Noise(float strength, float duration = 1)
    {
        _noiseAttribute.SetStrength(strength, duration);
    }

    public GameObject FindCenterTarget(ObjectType type, float findDist)
    {
        float viewingAngle = Camera.main.fieldOfView;
        Vector3 cmPos = Camera.main.transform.position;

        var data = GameManager.Instance.FieldObject.GetData(type)
            .Where(t => Vector3.Distance(t.Target.transform.position, cmPos) < findDist)
            .Where(t => 
            {
                Vector3 tPos = t.Target.transform.position;
                float rad = Vector3.Dot((tPos - cmPos).normalized, Camera.main.transform.forward);
                float angle = Mathf.Acos(rad) * Mathf.Rad2Deg;
                
                if (viewingAngle > angle) return true;
                else return false;
            });

        GameObject obj = null;
        float saveDist = float.MaxValue;

        foreach (FieldObjectData.Data t in data)
        {
            float dist = Vector3.Distance(t.Target.transform.position, _user.position);
            if (saveDist > dist)
            {
                saveDist = dist;
                obj = t.Target;
            }
        }

        return obj;
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(CmManager);
}
