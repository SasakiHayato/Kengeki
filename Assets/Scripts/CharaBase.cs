using UnityEngine;

public enum ObjectType
{
    GameUser,
    Enemy,
    Object,
}

public abstract class CharaBase : MonoBehaviour
{

    [SerializeField] ObjectType _type = ObjectType.Object;

    protected CharaData Data { get; private set; }
    public ObjectType Type => _type;

    void Start() => SetUp();
 
    protected virtual void SetUp()
    {
        Data = new CharaData();
    }

    public class CharaData
    {
        public string Name;
        public int HP;
    }
}
