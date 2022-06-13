using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogViewer : MonoBehaviour
{
    List<Text> _txtList = new List<Text>();

    int _setID = 0;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Text text = transform.GetChild(i).GetComponent<Text>();
            text.text = "";
            _txtList.Add(text);
        }

        _setID = 0;
    }

    public void SetText(string text)
    {
        string set = text.Replace('/', '\n');
        _txtList[_setID].text = set;
        _setID++;

        if (_setID >= _txtList.Count)
        {
            _setID = 0;
        }
    }
}
