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

        Vector3 move = new Vector3(input.x, 0, input.y) * Data.Speed;
        move.y = _physicsBase.Gravity.y;

        CharacterController.Move(move * Time.deltaTime);
    }

    void Rotate()
    {

    }

    void Fire()
    {
        Debug.Log("Fire");
    }
}
