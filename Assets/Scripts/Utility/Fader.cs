using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// FadeÇÃä«óùÉNÉâÉX
/// </summary>

public class Fader
{
    float _startVal;
    float _endVal;
    float _duration;

    Image _fadeImage;

    public bool IsEndFade { get; private set; }

    const string CanvasName = "Fader_FadeCanvas";
    const string ImageName = "Fader_FadeImage";
    Vector2 Aspect = new Vector2(1600, 1000);

    public Fader(float startVal, float endVal, float duration = 1)
    {
        _startVal = startVal;
        _endVal = endVal;
        _duration = duration;

        IsEndFade = false;

        _fadeImage = CreateImage(CreateCanvas());
    }

    GameObject CreateCanvas()
    {
        GameObject obj = new GameObject(CanvasName);
        Canvas canvas = obj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        CanvasScaler scaler = obj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = Aspect;

        return obj;
    }

    Image CreateImage(GameObject canvas)
    {
        GameObject obj = new GameObject(ImageName);
        obj.transform.SetParent(canvas.transform);

        Image image = obj.AddComponent<Image>();
        image.color = Color.black;

        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.anchoredPosition = Vector2.zero;

        return image;
    }

    public void SetFade(Ease ease = Ease.Linear)
    {
        Color color = _fadeImage.color;
        color.a = _startVal;
        _fadeImage.color = color;
        
        _fadeImage.DOFade(_endVal, _duration)
            .SetEase(ease)
            .OnComplete(() => IsEndFade = true);
    }
}
