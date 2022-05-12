using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒQ[ƒ€‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class GameManager : SingletonAttribute<GameManager>
{
    public ObjectDataBase ObjectData { get; private set; }

    public override void SetUp()
    {
        GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
        ObjectData = Resources.Load<ObjectDataBase>("ObjectDataBase");
    }
}
