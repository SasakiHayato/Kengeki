using UnityEngine;
using System;
using SingletonAttribute;

/// <summary>
/// GamePad入力の管理クラス
/// </summary>

public class GamePadInputter : SingletonAttribute<GamePadInputter>
{
    public enum ValueType
    {
        PlayerMove,
        CmMove,
    }

    public GamePad Input { get; private set; }

    const float PlayerMoveDeadInput = 0.2f;

    public override void SetUp()
    {
        Input = new GamePad();
        Input.Enable();
    }

    public object GetValue(ValueType type)
    {
        object value = null;

        switch (type)
        {
            case ValueType.PlayerMove:
                Vector2 player = Input.Player.Move.ReadValue<Vector2>();

                if (Mathf.Abs(player.x) < PlayerMoveDeadInput) player.x = 0;
                if (Mathf.Abs(player.y) < PlayerMoveDeadInput) player.y = 0;

                value = player;

                break;
            case ValueType.CmMove:
                value = Input.Player.Look.ReadValue<Vector2>();

                break;
        }

        return value;
    }

    public static void Despose()
    {
        Instance.Input.Dispose();
    }
}
