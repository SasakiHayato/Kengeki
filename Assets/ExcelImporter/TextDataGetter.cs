using UnityEngine;
using System.Linq;

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
        return dataList.First(d => d.ID == id).Text;
    }
}
