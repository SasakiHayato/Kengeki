using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

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
