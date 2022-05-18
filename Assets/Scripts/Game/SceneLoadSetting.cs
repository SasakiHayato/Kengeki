using UnityEngine;

/// <summary>
/// �Q�[���V�[���̊Ǘ��N���X
/// </summary>

public class SceneLoadSetting : MonoBehaviour
{
    [SerializeField] bool _isDebug;

    private void Awake()
    {
        GameManager.SetInstance(new GameManager()).SetUp();
        Effects.SetInstance(new Effects()).SetUp();

        if (!_isDebug)
        {
            GameManager.Instance.SetMapData();
        }
    }
}
