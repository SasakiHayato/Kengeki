using UnityEngine;

/// <summary>
/// Manager�Ɍp��������N���X
/// </summary>

public abstract class ManagerBase : MonoBehaviour
{
    [SerializeField] int _priority;

    public int Priority => _priority;
    public bool IsSetUp { get; private set; }

    private void Start()
    {
        IsSetUp = false;
        GameManager.Instance.AddManager(this);
    }

    public virtual void SetUp()
    {
        Debug.Log($"ManagerName {ManagerPath()} Priority {_priority}");
        IsSetUp = true;
    }

    public abstract GameObject ManagerObject();
    public abstract string ManagerPath();
}
