using UnityEngine;
using StateMachine;
using System.Linq;

public class CmManager : MonoBehaviour, IManager
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
    RadialBlurAttribute _radialrAttribute;

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
        if (_user == null)
        {
            _user = GameManager.Instance.Player.transform;
        }

        CmData = new Data(_user, _deadInput, _sensitivity);

        Vector3 offestPos = _user.position + _offsetPosition;
        transform.position = offestPos;

        _state = new StateManager(gameObject);
        _state.AddState(new NormalCm(), State.Normal)
            .AddState(new LockonCm(), State.Lockon)
            .AddState(new TransitionCm(), State.Transition)
            .AddState(new ShakeCm(), State.Shake)
            .RunRequest(true, State.Normal);

        _virtualityCm = new GameObject("Virtuliry").transform;

        ViewTarget = _user;
        CmData.VirticalRate = float.MaxValue;

        GameManager.Instance.AddManager(this);

        _radialrAttribute = GetComponent<RadialBlurAttribute>();
    }

    void Update()
    {
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
        transform.rotation = rotate;

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

    public void RadialBlur(float strength)
    {
        _radialrAttribute.SetStrength(strength);
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

    public GameObject ManagerObject() => gameObject;
    public string ManagerPath() => nameof(CmManager);
}
