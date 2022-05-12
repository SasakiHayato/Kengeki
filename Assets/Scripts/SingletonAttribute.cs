
/// <summary>
/// Singleton�ɂ���class�̊��N���X
/// </summary>
/// <typeparam name="Singleton">Singleton�ɂ���N���X</typeparam>

public abstract class SingletonAttribute<Singleton> where Singleton : class
{
    public static Singleton Instance { get; private set; } = null;
   
    public static Singleton SetInstance(Singleton singleton, bool reset = false)
    {
        if (Instance == null || reset == true)
        {
            Instance = singleton;
            UnityEngine.Debug.Log($"SetInstance => {Instance}");
        }

        return Instance;
    }

    public static void Delete()
    {
        UnityEngine.Debug.Log($"DeleteInstance => {Instance}");
        Instance = null;
    }

    public abstract void SetUp();
}
