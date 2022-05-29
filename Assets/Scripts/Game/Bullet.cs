using UnityEngine;

public class Bullet : MonoBehaviour, IPool
{   
    public bool Waiting { get; set; }

    Rigidbody _rb;

    float _speed;
    int _power;
    Vector3 _dir;
    ObjectType _objectType;

    float _timer;

    const float ActiveTime = 4f;

    public void SetData(ObjectType type, Vector3 dir, float speed, int power)
    {
        _speed = speed;
        _power = power;
        _dir = dir;
        _objectType = type;
    }

    public void SetUp(Transform parent)
    {
        gameObject.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        GetComponent<Collider>().isTrigger = true;
    }

    public bool Execute()
    {
        _timer += Time.deltaTime;
        _rb.velocity = _dir * _speed;

        return _timer < ActiveTime;
    }

    public void IsUseSetUp()
    {
        gameObject.SetActive(true);
        _timer = 0;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharaBase chara = other.GetComponent<CharaBase>();

        if (chara != null && chara.CharaData.ObjectType != _objectType)
        {
            IDamage iDamage = other.GetComponent<IDamage>();

            if (iDamage != null)
            {
                if (iDamage.GetDamage(_power))
                {
                    Effects.Instance.RequestParticleEffect(ParticalType.Hit, other.transform);
                    Waiting = true;
                    Delete();
                }
            }
        }
    }
}
