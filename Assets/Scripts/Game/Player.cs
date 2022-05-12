using UnityEngine;

/// <summary>
/// Playerä«óùÉNÉâÉX
/// </summary>

public class Player : CharaBase
{
    protected override void SetUp()
    {
        base.SetUp();

        Data.SetData(GameManager.Instance.ObjectData.GetData("Player"));

        GamePadInputter.Instance.Input.Player.Fire.started += context => Fire();
        GamePadInputter.Instance.SetAction(() => Move());
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
        
    }
}
