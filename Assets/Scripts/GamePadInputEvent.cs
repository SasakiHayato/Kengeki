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

public enum InputEventsType
{
    Title,
    Option,
    OptionSystem,
    OptionSound,
}

public class GamePadInputEvent : MonoBehaviour
{
    [Serializable]
    class EventData
    {
        [SerializeField] int _id;
        [SerializeField] Button _button;
        [SerializeReference, SubclassSelector]
        List<IInputEvents> _inputEvents;

        public int ID => _id;
        public Button Button => _button;
        public List<IInputEvents> InputEvents => _inputEvents;
    }

    [SerializeField] InputEventsType _inputEventsType;
    [SerializeField] List<EventData> _eventsData;

    public InputEventsType InputEventsType => _inputEventsType;

    int _selectID;
    int _saveInput;

    EventData _saveEvent;

    const float WaitSeconds = 0.5f;
    Vector3 ConstScale = new Vector3(1.2f, 1.2f, 1);

    void Awake()
    {
        foreach (EventData data in _eventsData)
        {
            data.Button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(WaitSeconds))
                .TakeUntilDestroy(data.Button)
                .Subscribe(_ => data.InputEvents.ForEach(c => c.Execute()));
        }

        if (GamePadInputter.Instance == null)
        {
            GamePadInputter.SetInstance(new GamePadInputter()).SetUp();
        }

        GamePadInputter.Instance.AddGamePadEvent(this);
    }

    public void Select(Vector2 input)
    {
        if ((int)input.y < 0 && _saveInput != -1)
        {
            _selectID++;
            if (_selectID >= _eventsData.Count) _selectID--;
            
            _saveInput = -1;
        }
        else if ((int)input.y > 0 && _saveInput != 1)
        {
            _selectID--;
            if (_selectID < 0) _selectID = 0;

            _saveInput = 1;
        }
        else if ((int)input.y == 0 && _saveInput != 0)
        {
            _saveInput = 0;
        }

        SetScale();
    }

    void SetScale()
    {
        foreach (EventData data in _eventsData)
        {
            if (data.ID == _selectID)
            {
                data.Button.transform.localScale = ConstScale;
                _saveEvent = data;
            }
            else
            {
                data.Button.transform.localScale = Vector3.one;
            }
        }
    }

    public void IsSelect()
    {
        _saveEvent.InputEvents.ForEach(c => c.Execute());
    }
}
