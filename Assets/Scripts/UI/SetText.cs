using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ゲームUIの子クラス。ExcelからのTextDataを表示するクラス
/// </summary>

public class SetText : ChildrenUI
{
    [SerializeField] Text _txt;
    Image _image;

    List<string> _txtList = new List<string>();

    bool _isDisplaying = false;
    const float Duration = 2f;

    public override void SetUp()
    {
        _image = GetComponent<Image>();
        _image.gameObject.SetActive(false);
        _txt.text = "";
    }

    public override void CallBack(object[] datas = null)
    {
        _txtList.Add((string)datas[0]);
        if (_isDisplaying) return;

        _isDisplaying = true;

        string get = _txtList.First();
        
        Set(get.Replace('/', '\n'));
    }

    void Set(string set)
    {
        _image.gameObject.SetActive(true);
        _txt.DOText(set, Duration)
            
            .OnComplete(() => Init().Forget());
    }

    async UniTask Init()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _txt.text = "";
        _isDisplaying = false;
        _txtList.Remove(_txtList.First());

        if (_txtList.Count > 0)
        {
            Set(_txtList.First());
        }
        else
        {
            _image.gameObject.SetActive(false);
        }
    }
}
