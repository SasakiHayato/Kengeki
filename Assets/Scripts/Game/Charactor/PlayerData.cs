using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public bool OnBerserker { get; private set; }
    public void SetOnBerserker(float effectTime)
    {
        OnBerserker = true;

    }
}
