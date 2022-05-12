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

    StateManager _state;

    protected override void SetUp()
    {
        base.SetUp();

        Data.SetData(GameManager.Instance.ObjectData.GetData("Player"));

        GamePadInputter.Instance.Input.Player.Fire.started += context => Fire();
        GamePadInputter.Instance.SetAction(() => Move());

        _state = new StateManager(gameObject);
        _state.AddState(new PlayerIdle(), State.Idle)
            .AddState(new PlayerMove(), State.Move)
            .RunRequest(true, State.Move);
    }

    void Update()
    {
        _state.Run();
    }

    void Move()
    {
        Vector2 input = (Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove);

        Vector3 move = new Vector3(input.x, 0, input.y) * Data.Speed;
        move.y = 0;

        CharacterController.Move(move * Time.deltaTime);
    }

    void Fire()
    {
        Debug.Log("Fire");
    }
}
