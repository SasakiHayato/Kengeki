using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingletonAttribute;

/// <summary>
/// ゲームの管理クラス
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
