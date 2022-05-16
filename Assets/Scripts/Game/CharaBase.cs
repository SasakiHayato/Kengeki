using UnityEngine;

public enum ObjectType
{
    GameUser,
    Enemy,
    Object,
}

public interface IDamage
{
    void GetDamage(int damage);
}

/// <summary>
/// キャラクターの基底クラス
/// </summary>

[RequireComponent(typeof(CharacterController))]
public abstract class CharaBase : MonoBehaviour
{
    [SerializeField] string _dataPath;
    [SerializeField] Transform _offsetPosition;
    
    protected CharacterController CharacterController { get; private set; }
    public CharaData Data { get; private set; }
    
    public ObjectAnimController Anim { get; private set; }
    public Transform OffsetPosition
    {
        get
        {
            if (_offsetPosition != null) return _offsetPosition;
            else return transform;
        }
    }
   
    void Start() => SetUp();
 
    protected virtual void SetUp()
    {
        CharacterController = GetComponent<CharacterController>();
        CharacterController.center = OffsetPosition.localPosition;

        ObjectDataBase.Data data = GameManager.Instance.ObjectData.GetData(_dataPath);
        Data = new CharaData();
        Data.SetData(data);

        Anim = new ObjectAnimController(data.Runtime, data.Avatar, gameObject);

        GameManager.Instance.FieldObject.Add(gameObject, data.ObjectType);
    }
}

public class CharaData
{
    public string Name { get; private set; }
    public int HP { get; private set; }
    public float Speed { get; private set; }
    public float DefaultSpeed { get; private set; }
    public ObjectType ObjectType;

    public void SetData(ObjectDataBase.Data data)
    {
        Name = data.Name;
        HP = data.HP;
        Speed = data.Speed;
        DefaultSpeed = data.Speed;
        ObjectType = data.ObjectType;
    }

    public void UpdateSpeed(float value)
    {
        Speed = value;
    }
}
