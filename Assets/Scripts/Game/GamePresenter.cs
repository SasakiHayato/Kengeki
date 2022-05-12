using UnityEngine;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetInstance(new GameManager()).SetUp();
    }

    void Update()
    {
        GamePadInputter.Instance.Update();
    }
}
