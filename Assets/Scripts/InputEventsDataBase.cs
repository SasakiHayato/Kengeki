using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InputEventsDataBase")]
public class InputEventsDataBase : ScriptableObject
{
    [SerializeField] List<EventData> _datas;

    public List<EventData> DataList => _datas;
    public EventData GetData(int id) => _datas[id];

    [System.Serializable]
    public class EventData
    {
        [SerializeField] int _groupID;
        [SerializeField] int _id;
        [SerializeField] Button _button;
        [SerializeReference, SubclassSelector]
        IInputEvents _inputEvents;

        public int GroupID => _groupID;
        public int ID => _id;
        public Button Button => _button;
        public IInputEvents InputEvents => _inputEvents;
    }
}
