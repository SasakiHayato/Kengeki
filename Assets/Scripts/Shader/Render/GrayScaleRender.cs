using UnityEngine;
using DG.Tweening;

/// <summary>
/// GrayScaleShaderの使用リクエストクラス
/// </summary>

[ExecuteInEditMode]
public class GrayScaleRender : MonoBehaviour
{
    [SerializeField] Shader _shader;
    [SerializeField, Range(0, 1)] float _strength;

    Material _material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_shader == null)
        {
            Graphics.Blit(source, destination);
            return;
        }
        else
        {
            _material = new Material(_shader);
        }

        _material.SetFloat("_Strength", _strength);
        Graphics.Blit(source, destination, _material);
    }

    public void SetStrength(float strength, float duration)
    {
        DOTween.To
            (
                () => _strength,
                (x) => _strength = x,
                strength,
                duration
            );
    }
}
