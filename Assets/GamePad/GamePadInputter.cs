using UnityEngine;
using SingletonAttribute;

public enum InputterType
{
    Player,
    UI,
}
/// <summary>
/// GamePad“ü—Í‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class GamePadInputter : SingletonAttribute<GamePadInputter>
{
    public enum ValueType
    {
        PlayerMove,
        CmMove,

        UINavigate,
    }

    public GamePad Input { get; private set; }

    public InputterType CurrentInputterType { get; private set; }

    const float DeadInput = 0.2f;

    public override void SetUp()
    {
        Input = new GamePad();
        Input.Enable();
    }

    public Vector2 GetValue(ValueType type)
    {
        Vector2 value = Vector2.zero;

        switch (type)
        {
            case ValueType.PlayerMove:
                Vector2 player = Input.Player.Move.ReadValue<Vector2>();

                if (Mathf.Abs(player.x) < DeadInput) player.x = 0;
                if (Mathf.Abs(player.y) < DeadInput) player.y = 0;

                value = player;

                break;
            case ValueType.CmMove:
                value = Input.Player.Look.ReadValue<Vector2>();

                break;

            case ValueType.UINavigate:
                value = Input.UI.Navigate.ReadValue<Vector2>();

                break;
        }

        return value;
    }

    public void SetInputterType(InputterType type) => CurrentInputterType = type;

    public static void Despose()
    {
        Instance.Input.Dispose();
    }
}
