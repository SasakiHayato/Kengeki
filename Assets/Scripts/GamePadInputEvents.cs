using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UniRx;
using System;

public interface IInputEvents
{
    void SetUp();
    void Execute();
}

public class GamePadInputEvents : MonoBehaviour
{
    [Serializable]
    class EventData
    {
        [SerializeField] int _id;
        [SerializeField] Button _button;
        [SerializeReference, SubclassSelector]
        IInputEvents _inputEvents;

        public int ID => _id;
        public Button Button => _button;
        public IInputEvents InputEvents => _inputEvents;
    }

    [SerializeField] List<EventData> _eventsData;

    int _selectID;

    const float WaitSeconds = 0.5f;

    void Start()
    {
        foreach (EventData data in _eventsData)
        {
            data.Button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(WaitSeconds))
                .TakeUntilDestroy(data.Button)
                .Subscribe(_ => data.InputEvents.Execute());
        }
    }

    void Update()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.UI) return;

        Vector2 input = GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.UINavigate);
        Select(input);
    }

    void Select(Vector2 input)
    {

    }
}
