using UnityEngine;

/// <summary>
/// シーン開始時のセットアップクラス
/// </summary>

public class GamePresenter : MonoBehaviour
{
    private void Awake()
    {
        GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
    }
}
