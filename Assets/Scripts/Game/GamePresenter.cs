using UnityEngine;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    private void Awake()
    {
        GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
    }

    void Update()
    {
        GamePadInputter.Instance.Update();
    }
}
