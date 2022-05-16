using UnityEngine;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetInstance(new GameManager()).SetUp();
        Effects.SetInstance(new Effects()).SetUp();
    }
}
