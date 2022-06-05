using UnityEngine;

/// <summary>
/// �΍��ˌ��̌v�Z
/// </summary>

public struct Deviation
{
    // tPos => Player
    // tBeforePos => Player��1F.later
    // myPos => Enemy
    // speed => Bullet�̑��x
    public Vector3 DeviationDir(Vector3 tPos, Vector3 myPos, Vector3 tBeforePos, float speed)
    {
        float fps = 1 / Time.deltaTime;
        // Bullet�̓��B���ԁ@�b
        float t = Vector3.Distance(myPos, tPos) / speed;

        // Player�̕���
        Vector3 targetDir = (tPos - tBeforePos).normalized;

        // Player��1�b�̑��x

        float tSpeed = Vector3.Distance(tPos, tBeforePos) * fps;

        // �\���ʒu
        Vector3 predictPos = tPos + (targetDir * tSpeed) * t;
        if (predictPos == tPos) return (predictPos - myPos).normalized;

        return (predictPos - myPos).normalized;
    }
}