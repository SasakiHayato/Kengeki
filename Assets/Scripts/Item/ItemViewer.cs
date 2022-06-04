using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemViewer : MonoBehaviour
{
    [SerializeField] Image _itemButtonPanel;
    [SerializeField] Text _msgText;

    class ButtonData
    {
        int ID;
        Text Text;
    }

    void Start()
    {
        
    }
}
