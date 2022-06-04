
public class InputEventStopBGM : IInputEvents
{
    SoundManager _soundManager;

    public void SetUp()
    {
        _soundManager = GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager));
    }

    public void Execute()
    {
        _soundManager.StopBGM();
    }
}
