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
    [SerializeField] float _dist;
    [SerializeField, Range(0, 1.0f)] float _deadInput;
    [SerializeField] float _moveDelay;
    [SerializeField] float _viewDelay;

    float _moveTimer;
    float _viewTimer;

    StateManager _state;
    public Data CmData { get; private set; }

    public class Data
    {
        public Data(Transform user, Vector3 offsetPos, float deadIput)
        {
            User = user;
            OffsetPosition = offsetPos;
            DeadInput = deadIput;
        }

        public Transform User { get; private set; }
        public Vector3 OffsetPosition { get; private set; }
        public float DeadInput { get; private set; }

        public Vector3 Position { get; set; }
    }

    void Start()
    {
        _state = new StateManager(gameObject);
        _state.AddState(new NormalCm(), State.Normal)
            .AddState(new LockonCm(), State.Lockon)
            .AddState(new TransitionCm(), State.Transition)
            .RunRequest(true, State.Normal);

        CmData = new Data(transform, _offsetPosition, _deadInput);
    }

    void Update()
    {
        _state.Run();

        View();
        Move();
    }

    void Move()
    {
        _moveTimer += Time.deltaTime / _moveDelay;

        Vector3 setPos = Vector3.Slerp(transform.position.normalized, CmData.Position.normalized, _moveTimer);
        transform.position = (setPos * (_dist - Zoom())) + _user.position;

        if (setPos == transform.position) _moveTimer = 0;
    }

    void View()
    {
        _viewTimer += Time.deltaTime / _viewDelay;

        Vector3 dir = _user.position - transform.position;
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
