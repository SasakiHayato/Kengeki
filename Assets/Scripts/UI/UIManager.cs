using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : ManagerBase
{
    [SerializeField] List<GamePadInputEvent> _eventList; 

    void Start()
    {
        GameManager.Instance.AddManager(this);
    }

    public override void SetUp()
    {
        _eventList.ForEach(e => e.SetUp());

        base.SetUp();
    }

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(UIManager);
}
