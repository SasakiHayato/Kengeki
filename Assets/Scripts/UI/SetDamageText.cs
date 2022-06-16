using UnityEngine;

/// <summary>
/// ゲームUIの子クラス。ダメージの表示するクラス
/// </summary>

public class SetDamageText : ChildrenUI
{
    [SerializeField] TextPool _text;

    ObjectPool<TextPool> _textPool;

    public override void SetUp()
    {
        _textPool = new ObjectPool<TextPool>(_text);
    }

    public override void CallBack(object[] datas = null)
    {
        int damage = (int)datas[0];
        Transform t = (Transform)datas[1];

        TextPool text = _textPool.Use();
        text.SetData(damage, t);
    }
}
