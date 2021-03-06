using UnityEngine;
using UniRx;

public enum ObjectType
{
    GameUser,
    Enemy,
    Object,
}

public interface IDamage
{
    bool GetDamage(int damage);
}

/// <summary>
/// キャラクターの基底クラス
/// </summary>

[RequireComponent(typeof(PhysicsBase))]
public abstract class CharaBase : MonoBehaviour
{
    [SerializeField] string _dataPath;
    [SerializeField] Transform _offsetPosition;
    
    protected Rigidbody RB { get; private set; }
    protected PhysicsBase PhysicsBase { get; private set; }
    public CharaData CharaData { get; private set; } = new CharaData();
    
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
        PhysicsBase = GetComponent<PhysicsBase>();

        RB = GetComponent<Rigidbody>();
        RB.useGravity = false;
        RB.freezeRotation = true;

        ObjectDataBase.Data data = GameManager.Instance.ObjectData.GetData(_dataPath);
        CharaData.SetData(data);

        if (data.Runtime != null)
        {
            Anim = new ObjectAnimController(data.Runtime, data.Avatar, gameObject);
        }

        GameManager.Instance.FieldObject.Add(gameObject, data.ObjectType, CharaData.ID);
    }

    protected virtual void DestoryRequest()
    {
        Effects.Instance.RequestParticleEffect(ParticalType.Dead, _offsetPosition);
        GameManager.Instance.FieldObject.Remove(CharaData.ObjectType, CharaData.ID);

        Destroy(gameObject);
    }
}

public class CharaData
{
    static int _id;

    public int ID { get; private set; }
    public string Name { get; private set; }
    public int HP { get; private set; }
    public int MaxHP;
    public float Speed { get; private set; }
    public int Power { get; private set; }
    public float DefaultSpeed { get; private set; }
    public ObjectType ObjectType;

    public void SetData(ObjectDataBase.Data data)
    {
        Name = data.Name;
        HP = data.HP;
        MaxHP = data.HP;
        Speed = data.Speed;
        Power = data.Power;
        DefaultSpeed = data.Speed;
        ObjectType = data.ObjectType;

        ID = _id;
        _id++;
    }

    public void UpdateSpeed(float value)
    {
        Speed = value;
    }

    public void UpdateHP(int hp)
    {
        HP = hp;
        if (HP >= MaxHP) HP = MaxHP;
    }

    public void UpdateMaxHP(int hp, bool isAttributeHP = false)
    {
        MaxHP = hp;

        if (isAttributeHP)
        {
            HP = hp;
        }
    }

    public void UpdatePower(int power)
    {
        Power = power;
    }
}
