using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleporter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        CharaBase charaBase = collision.gameObject.GetComponent<CharaBase>();

        if (charaBase.CharaData.ObjectType == ObjectType.GameUser)
        {
            GameManager.Instance.ChangeScene("GameScene");
        }
    }
}
