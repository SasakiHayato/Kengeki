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
    [SerializeField] string _dataPath;

    protected CharacterController CharacterController { get; private set; }
    protected CharaData Data { get; private set; }

    public ObjectAnimController Anim { get; private set; }
   
    void Start() => SetUp();
 
    protected virtual void SetUp()
    {
        Data = new CharaData();
        CharacterController = GetComponent<CharacterController>();

        ObjectDataBase.Data data = GameManager.Instance.ObjectData.GetData(_dataPath);

        Data.SetData(data);
        Anim = new ObjectAnimController(data.Runtime, data.Avatar, gameObject);
    }

    public class CharaData
    {
        public string Name { get; private set; }
        public int HP { get; private set; }
        public float Speed { get; private set; }
        public ObjectType ObjectType;

        public void SetData(ObjectDataBase.Data data)
        {
            Name = data.Name;
            HP = data.HP;
            Speed = data.Speed;
            ObjectType = data.ObjectType;
        }
    }
}
