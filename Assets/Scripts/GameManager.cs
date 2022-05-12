using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SingletonAttribute;

/// <summary>
/// �Q�[���̊Ǘ��N���X
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
