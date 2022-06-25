using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLockonUI : ChildrenUI
{
    Image _image;
    RectTransform _rect;

    Transform _target;

    public override void SetUp()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;

        _rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (_target == null) return;

        _rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.position);
    }

    public override void CallBack(object[] datas = null)
    {
        Transform target = (Transform)datas[0];

        if (target != null)
        {
            _image.enabled = true;
            _target = target;   
        }
        else
        {
            _image.enabled = false;
            _target = null;
        }
    }
}
