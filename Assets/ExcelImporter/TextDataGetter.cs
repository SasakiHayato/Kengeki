using UnityEngine;
using System.Linq;

/// <summary>
/// Excelからのデータを受け取るクラス
/// </summary>

public class TextDataGetter
{
    KengekiTextData _textData;

    const string DataPath = "KengekiTextData";

    public void SetUp()
    {
        _textData = Resources.Load<KengekiTextData>(DataPath);
    }

    public string Request(string path, int id)
    {
        var dataList = _textData.TextData.Where(d => d.Path == path);
        string txt = dataList.First(d => d.ID == id).Text;
        GameManager.Instance.GetManager<UIManager>(nameof(UIManager)).ReqestSetLog(txt);
        return txt;
    }
}
