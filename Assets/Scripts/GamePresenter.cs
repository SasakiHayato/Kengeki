using UnityEngine;

/// <summary>
/// �V�[���J�n���̃Z�b�g�A�b�v�N���X
/// </summary>

public class GamePresenter : MonoBehaviour
{
    private void Awake()
    {
        GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
    }
}
