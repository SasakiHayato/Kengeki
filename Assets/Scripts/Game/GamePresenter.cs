using UnityEngine;

/// <summary>
/// �Q�[���V�[���̊Ǘ��N���X
/// </summary>

public class GamePresenter : MonoBehaviour
{
    [SerializeField] GameState _gameState;
    [SerializeField] InputterType _inputterType;
    [SerializeField] bool _isDebug;

    private void Awake()
    {
        GameManager.SetInstance(new GameManager()).SetUp();
        GameManager.Instance.SetGameState(_gameState);
        
        if (!_isDebug || GameManager.Instance.CurrentGameState == GameState.InGame)
        {
            GameManager.Instance.SetMapData();
        }

        BaseUI.SetInstance(new BaseUI()).SetUp();
        Effects.SetInstance(new Effects()).SetUp();

        if (GamePadInputter.Instance == null)
        {
            GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
        }

        GamePadInputter.Instance.SetInputterType(_inputterType);
    }

    private void Start()
    {
        GamePadInputter.Instance.RequestGamePadEvents(InputEventsType.Title);
    }

    void Update()
    {
        GamePadInputter.Instance.UIInputUpdate();
    }
}