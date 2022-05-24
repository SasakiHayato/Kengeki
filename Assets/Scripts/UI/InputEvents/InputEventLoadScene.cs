using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(_sceneName);
        }
    }
}
