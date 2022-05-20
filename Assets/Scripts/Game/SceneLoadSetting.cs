using UnityEngine;

/// <summary>
/// ゲームシーンの管理クラス
/// </summary>

public class SceneLoadSetting : MonoBehaviour
{
    [SerializeField] bool _isDebug;

    private void Awake()
    {
        GameManager.SetInstance(new GameManager()).SetUp();

        

        if (!_isDebug)
        {
            GameManager.Instance.SetMapData();
        }

        BaseUI.SetInstance(new BaseUI()).SetUp();
        Effects.SetInstance(new Effects()).SetUp();
    }
}
