using UnityEngine;
using System;
using SingletonAttribute;

/// <summary>
/// GamePadì¸óÕÇÃä«óùÉNÉâÉX
/// </summary>

public class GamePadInputter : SingletonAttribute<GamePadInputter>
{
    public enum ValueType
    {
        PlayerMove,
        CmMove,
    }

    public GamePad Input { get; private set; }

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
                value = Input.Player.Move.ReadValue<Vector2>();

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
