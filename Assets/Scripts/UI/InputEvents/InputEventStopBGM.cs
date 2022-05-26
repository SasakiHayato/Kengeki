
public class InputEventStopBGM : IInputEvents
{
    public void SetUp()
    {
        
    }

    public void Execute()
    {
        GameManager.Instance.GetManager<SoundManager>(nameof(SoundManager)).StopBGM();
    }
}
