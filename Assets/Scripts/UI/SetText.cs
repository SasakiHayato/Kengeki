using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

public class SetText : ChildrenUI
{
    [SerializeField] Text _txt;
    Image _image;

    const float Duration = 2f;

    public override void SetUp()
    {
        _image = GetComponent<Image>();
        _image.gameObject.SetActive(false);
        _txt.text = "";
    }

    public override void CallBack(object[] datas = null)
    {
        _image.gameObject.SetActive(true);
        _txt.DOText((string)datas[0], Duration)
            .OnComplete(() => Init().Forget());
    }

    async UniTask Init()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _txt.text = "";
        _image.gameObject.SetActive(false);
    }
}
