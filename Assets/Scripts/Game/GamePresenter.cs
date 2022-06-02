using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    [SerializeField] GameState _gameState;
    [SerializeField] InputterType _inputterType;
    
    bool _isSetUp = false;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.SetInstance(new GameManager()).SetUp();
        }
        
        GameManager.Instance.SetGameState(_gameState);
        
        BaseUI.SetInstance(new BaseUI()).SetUp();
        Effects.SetInstance(new Effects()).SetUp();
        
        GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
        GamePadInputter.Instance.SetInputterType(_inputterType);
    }

    void Start()
    {
        WaitSetUpManager().Forget();
        WaitFade().Forget();
    }

    async UniTask WaitFade()
    {
        Fader fader = new Fader(1, 0);
        await UniTask.WaitUntil(() => _isSetUp);

        
        fader.SetFade();

        await UniTask.WaitUntil(() => fader.IsEndFade);

        if (GameManager.Instance.CurrentGameState == GameState.InGame)
        {
            GamePadInputter.Instance.RequestGamePadEvents(InputEventsType.Option);
        }
        else
        {
            GamePadInputter.Instance.RequestGamePadEvents(InputEventsType.Title);
        }

    }

    async UniTask WaitSetUpManager()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        GameManager.Instance.SetUpManager();
        _isSetUp = true;
    }

    void Update()
    {
        GamePadInputter.Instance.UIInputUpdate();
    }
}
