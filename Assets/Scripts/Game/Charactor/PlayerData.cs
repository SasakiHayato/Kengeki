/// <summary>
/// playerのデータクラス
/// </summary>

public class PlayerData 
{
    public bool OnBerserker { get; private set; }
    public void SetOnBerserker(float effectTime)
    {
        OnBerserker = true;

    }
}
