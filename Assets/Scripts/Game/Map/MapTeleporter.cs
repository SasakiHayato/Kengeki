using UnityEngine;

/// <summary>
/// TeleporterCell‚ÌŠÇ—ƒNƒ‰ƒX
/// </summary>

public class MapTeleporter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        CharaBase charaBase = collision.gameObject.GetComponent<CharaBase>();

        if (charaBase.CharaData.ObjectType == ObjectType.GameUser)
        {
            if (GameManager.Instance.CurrentMapType == MapType.Arena)
            {
                GameManager.Instance.ChangeScene("TitleScene");
            }
            else
            {
                GameManager.Instance.ChangeScene("GameScene");
            }
        }
    }
}
