using UnityEngine;
using DG.Tweening;

[ExecuteInEditMode]
public class NoiseRenderer : MonoBehaviour
{
    [SerializeField] Shader _shader;
    [SerializeField, Range(0, 1)] float _horizonValue;

    Material _material;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }
        // ランダムシード値を更新することで乱数を動かす
        _material.SetInt("_Seed", Time.frameCount);
        // 左右にずらす値をセット
        _material.SetFloat("_HorizonValue", _horizonValue);
        Graphics.Blit(src, dest, _material);
    }

    public void SetStrength(float strength, float duration)
    {
        DOTween.To
            (
                () => _horizonValue,
                (x) => _horizonValue = x,
                strength,
                duration
            )
            .OnComplete(() => _horizonValue = 0);
    }
}
