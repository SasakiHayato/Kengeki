using UnityEngine;

/// <summary>
/// �Q�[���V�[���̊Ǘ��N���X
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
