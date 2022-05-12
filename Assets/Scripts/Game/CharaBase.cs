using UnityEngine;

public enum ObjectType
{
    GameUser,
    Enemy,
    Object,
}

[RequireComponent(typeof(CharacterController))]
public abstract class CharaBase : MonoBehaviour
{
    [SerializeField] ObjectType _type = ObjectType.Object;

    protected CharacterController CharacterController { get; private set; }

    protected CharaData Data { get; private set; }
    public ObjectType Type => _type;

    void Start() => SetUp();
 
    protected virtual void SetUp()
    {
        Data = new CharaData();
        CharacterController = GetComponent<CharacterController>();   
    }

    public class CharaData
    {
        public string Name { get; private set; }
        public int HP { get; private set; }
        public float Speed { get; private set; }

        public void SetData(ObjectDataBase.Data data)
        {
            Name = data.Name;
            HP = data.HP;
            Speed = data.Speed;
        }
    }
}
