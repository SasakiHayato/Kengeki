using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharaBase
{
    protected override void SetUp()
    {
        base.SetUp();

        GamePadInputter.Instance.Input.Player.Fire.started += context => Fire();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {

    }

    void Fire()
    {
        
    }
}
