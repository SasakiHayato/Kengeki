using UnityEngine;
using StateMachine;

/// <summary>
/// Playerä«óùÉNÉâÉX
/// </summary>

public class Player : CharaBase
{
    public enum State
    {
        Idle,
        Move,
    }

    Vector3 _beforePos;

    PhysicsBase _physicsBase;
    JumpSetting _jumpSetting;
    StateManager _state;

    protected override void SetUp()
    {
        base.SetUp();

        _physicsBase = GetComponent<PhysicsBase>();
        _jumpSetting = GetComponent<JumpSetting>();

        GamePadInputter.Instance.Input.Player.Fire.performed += context => Lockon();
        GamePadInputter.Instance.Input.Player.Jump.performed += context => Jump();
        
        _state = new StateManager(gameObject);
        _state.AddState(new PlayerIdle(), State.Idle)
            .AddState(new PlayerMove(), State.Move)
            .RunRequest(true, State.Idle);

        _beforePos = transform.position;
    }

    void Update()
    {
        _state.Run();

        Move();
        Rotate();
    }

    void Move()
    {
        Vector2 input = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove);

        Vector3 forward = Camera.main.transform.forward * input.y;
        Vector3 right = Camera.main.transform.right * input.x;

        Vector3 move = (forward + right) * Data.Speed;
        move.y = 1 + _jumpSetting.Power * -1;

        Vector3 scale = Vector3.Scale(move, _physicsBase.Gravity);
        CharacterController.Move(scale * Time.deltaTime);
    }

    void Rotate()
    {
        Vector3 dir = transform.position - _beforePos;
        dir.y = 0;
        _beforePos = transform.position;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void Lockon()
    {
        if (GameManager.Instance.LockonTarget != null)
        {
            GameManager.Instance.LockonTarget = null;
        }
        else
        {
            GameObject data = GameManager.Instance.FieldObject.GetData(ObjectType.Enemy)[0].Target;
            GameManager.Instance.LockonTarget = data.transform;
        }
    }

    void Jump()
    {
        _jumpSetting.Set();
    }
}
