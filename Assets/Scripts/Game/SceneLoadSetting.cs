using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class SceneLoadSetting : MonoBehaviour
{
    [SerializeField] GameState _gameState;
    [SerializeField] InputterType _inputterType;
    [SerializeField] float _waitSetUpTime; 
    
    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.SetInstance(new GameManager()).SetUp();
        }
        
        GameManager.Instance.SetGameState(_gameState);
        
        
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
        await UniTask.WaitUntil(() => 
        {
            Debug.Log($"ManagerSetUp => {GameManager.Instance.ManagerIsSetUp}");
            return GameManager.Instance.ManagerIsSetUp;
        });

        fader.SetFade();

        await UniTask.WaitUntil(() =>
        {
            Debug.Log($"IsFade => {fader.IsEndFade}");
            return fader.IsEndFade;
        });

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
        await UniTask.Delay(TimeSpan.FromSeconds(_waitSetUpTime));
        GameManager.Instance.SetUpManager();
    }

    void Update()
    {
        if (!GameManager.Instance.ManagerIsSetUp) return;

        GamePadInputter.Instance.UIInputUpdate();
    }
}
