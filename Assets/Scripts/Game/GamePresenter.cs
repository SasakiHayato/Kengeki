using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    [SerializeField] GameState _gameState;
    [SerializeField] InputterType _inputterType;
    [SerializeField] bool _isDebug;

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
        WaitFade().Forget();
    }

    async UniTask WaitFade()
    {
        Fader fader = new Fader(1, 0);
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

    void Update()
    {
        GamePadInputter.Instance.UIInputUpdate();
    }
}
