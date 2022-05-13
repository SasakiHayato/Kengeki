using UnityEngine;
using StateMachine;

public class CmManager : MonoBehaviour
{
    public enum State
    {
        Normal,
        Lockon,
        Transition,
    }

    [SerializeField] Transform _user;
    [SerializeField] Vector3 _offsetPosition;
    [SerializeField] Vector3 _offsetView;
    [SerializeField] float _dist;
    [SerializeField, Range(0, 1.0f)] float _deadInput;
    [SerializeField] float _moveDelay;
    [SerializeField] float _viewDelay;
    [SerializeField] float _sensitivity;

    float _viewTimer;

    StateManager _state;
    public Data CmData { get; private set; }

    public class Data
    {
        public Data(Transform user, Vector3 offsetPos, float deadIput, float sensitivity)
        {
            User = user;
            OffsetPosition = offsetPos;
            DeadInput = deadIput;
            Sensitivity = sensitivity;
        }

        public Transform User { get; private set; }
        public Vector3 OffsetPosition { get; private set; }
        public float DeadInput { get; private set; }
        public float Sensitivity { get; private set; }

        Vector3 _normalPosition = Vector3.zero;
        public Vector3 NormalizePosition 
        {
            get => _normalPosition.normalized; 
            set { _normalPosition = value; }
        }
    }

    void Start()
    {
        CmData = new Data(_user, _offsetPosition, _deadInput, _sensitivity);

        Vector3 offestPos = _user.position + _offsetPosition;
        transform.position = offestPos;

        _state = new StateManager(gameObject);
        _state.AddState(new NormalCm(), State.Normal)
            .AddState(new LockonCm(), State.Lockon)
            .AddState(new TransitionCm(), State.Transition)
            .RunRequest(true, State.Normal);
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
        cmPos.y = CmData.NormalizePosition.y + _offsetPosition.y;
        transform.position = cmPos + _user.position;
    }

    void View()
    {
        _viewTimer += Time.deltaTime / _viewDelay;

        Vector3 dir = _user.position - transform.position + _offsetView;
        Quaternion forward = Quaternion.LookRotation(dir.normalized);
        Quaternion rotate = Quaternion.Lerp(transform.rotation, forward, _viewTimer);
        transform.rotation = rotate;

        if (rotate == transform.rotation) _viewTimer = 0;
    }

    float Zoom()
    {
        return 0;
    }
}
