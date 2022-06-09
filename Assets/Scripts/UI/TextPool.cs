using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextPool : MonoBehaviour, IPool
{
    public bool Waiting { get; set; }

    RectTransform _rect;
    Text _txt;

    bool _isUse = true;

    const float AddVal = 1f;
    const float DurationTime = 0.5f;

    public void SetUp(Transform parent)
    {
        _txt = GetComponent<Text>();
        _rect = GetComponent<RectTransform>();

        transform.SetParent(BaseUI.Instance.MasterCanvas.transform);
        gameObject.SetActive(false);
    }

    public void SetData(int damage, Transform target)
    {
        _rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        _txt.text = damage.ToString();

        _rect.DOAnchorPosY(_rect.position.y + AddVal, DurationTime)
            .SetEase(Ease.Linear)
            .OnComplete(() => _isUse = false);
    }

    public void IsUseSetUp()
    {
        gameObject.SetActive(true);
        _isUse = true;
    }

    public bool Execute()
    {
        return _isUse;
    }

    public void Delete()
    {
        _txt.text = "";
        gameObject.SetActive(false);
    }
}
