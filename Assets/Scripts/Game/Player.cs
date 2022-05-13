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
    StateManager _state;

    protected override void SetUp()
    {
        base.SetUp();

        _physicsBase = GetComponent<PhysicsBase>();

        GamePadInputter.Instance.Input.Player.Fire.started += context => Fire();
        
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
        move.y = _physicsBase.Gravity.y;

        CharacterController.Move(move * Time.deltaTime);
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

    void Fire()
    {
        Debug.Log("Fire");
    }
}
