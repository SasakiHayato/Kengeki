using UnityEngine;
using System.Collections.Generic;
using SingletonAttribute;
using System.Linq;

public enum InputterType
{
    Player,
    UI,
}
/// <summary>
/// GamePadì¸óÕÇÃä«óùÉNÉâÉX
/// </summary>

public class GamePadInputter : SingletonAttribute<GamePadInputter>
{
    public enum ValueType
    {
        PlayerMove,
        CmMove,

        UINavigate,
    }

    List<GamePadInputEvent> _gamePadInputEventList;
    GamePadInputEvent _gamePadInputEvent;

    public GamePad Input { get; private set; }

    public InputterType CurrentInputterType { get; private set; }
    
    const float DeadInput = 0.2f;

    public override void SetUp()
    {
        Input = new GamePad();
        Input.Enable();

        _gamePadInputEventList = new List<GamePadInputEvent>();
        
        Input.UI.Submit.performed += context => IsSelect();

        if (GameManager.Instance.CurrentGameState == GameState.InGame)
        {
            Input.UI.Option.performed += cotext => OpenOption();
            Input.UI.RightTrigger.performed += context => OnTrigger(GamePadInputEvent.TriggerType.Right);
            Input.UI.LeftTrigger.performed += context => OnTrigger(GamePadInputEvent.TriggerType.Left);
        }
    }

    void OpenOption()
    {
        SetInputterType(InputterType.UI);
        BaseUI.Instance.ParentActive("Option", true);
    }

    void OnTrigger(GamePadInputEvent.TriggerType type)
    {
        if (_gamePadInputEvent == null) return;

        _gamePadInputEvent.OnTrigger(type);
    }

    void IsSelect()
    {
        if (_gamePadInputEvent == null) return;

        _gamePadInputEvent.IsSelect();
    }

    public void UIInputUpdate()
    {
        if (InputterType.UI != CurrentInputterType || _gamePadInputEvent == null) return;

        Vector2 input = Input.UI.Navigate.ReadValue<Vector2>();

        if (Mathf.Abs(input.x) < DeadInput) input.x = 0;
        if (Mathf.Abs(input.y) < DeadInput) input.y = 0;

        _gamePadInputEvent.Select(input);
    }

    public Vector2 PlayerGetValue(ValueType type)
    {
        if (InputterType.Player != CurrentInputterType) return Vector2.zero;

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
        }

        return value;
    }

    public void SetInputterType(InputterType type)
    {
        CurrentInputterType = type;

        if (type == InputterType.Player)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void RequestGamePadEvents(InputEventsType type)
    {
        _gamePadInputEvent = _gamePadInputEventList.FirstOrDefault(g => g.InputEventsType == type);
    }

    public static void Despose()
    {
        Instance.Input.Dispose();
        Instance._gamePadInputEvent = null;
    }

    public void AddGamePadEvent(GamePadInputEvent events)
    {
        _gamePadInputEventList.Add(events);
    }
}
