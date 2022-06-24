using UnityEngine;
using System.Collections.Generic;
using SingletonAttribute;
using System.Linq;
using UnityEngine.InputSystem;

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

    public int SelectID { get; set; }
    public bool IsConnect { get; private set; }

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
        if (CurrentInputterType == InputterType.Player)
        {
            SetInputterType(InputterType.UI);
            BaseUI.Instance.ParentActive("Option", true);
        }
        else
        {
            SetInputterType(InputterType.Player);
            RequestGamePadEvents(InputEventsType.Option);
            BaseUI.Instance.ParentActive("Option", false);
            _gamePadInputEvent.Init();
        }
    }

    void OnTrigger(GamePadInputEvent.TriggerType type)
    {
        if (InputterType.UI != CurrentInputterType || _gamePadInputEvent == null) return;
        
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
        
        if (Gamepad.current != null)
        {
            IsConnect = true;
        }
        else
        {
            IsConnect = false;
        }
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
            if (GameManager.Instance.CurrentGameState != GameState.InGame) return;
            Time.timeScale = 0;
        }
    }

    public void RequestGamePadEvents(InputEventsType type)
    {
        _gamePadInputEvent = _gamePadInputEventList.FirstOrDefault(g => g.InputEventsType == type);
        _gamePadInputEvent.Init();
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
