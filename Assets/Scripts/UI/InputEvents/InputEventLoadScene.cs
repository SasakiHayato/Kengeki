using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// �Q�[���p�b�h����̓��͂ɑ΂�����s�N���X�BScene�ύX�̃��N�G�X�g
/// </summary>

public class InputEventLoadScene : IInputEvents
{
    [SerializeField] string _sceneName;
    [SerializeField] bool _isFade;

    public void SetUp()
    {
        
    }

    public void Execute()
    {
        if (!_isFade)
        {
            GameManager.Instance.ChangeScene(_sceneName);
        }
        else
        {
            WaitFade().Forget();
        }
    }

    async UniTask WaitFade()
    {
        Fader fader = new Fader(0, 1);
        fader.SetFade();
        await UniTask.WaitUntil(() => fader.IsEndFade);
        GameManager.Instance.ChangeScene(_sceneName);
    }
}
