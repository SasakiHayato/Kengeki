using UnityEngine;

/// <summary>
/// �Q�[���V�[���̊Ǘ��N���X
/// </summary>

public class GamePresenter : MonoBehaviour
{
    private void Awake()
    {
        GameManager.SetInstance(new GameManager()).SetUp();
        Effects.SetInstance(new Effects()).SetUp();
    }
}
