using UnityEngine;
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
    [SerializeField] List<InputEventsDataBase> _inputEventsDataBases;

    int _selectID;

    const float WaitSeconds = 0.5f;

    void Start()
    {
        foreach (InputEventsDataBase dataBase in _inputEventsDataBases)
        {
            dataBase.DataList.ForEach(data =>
            {
                data.Button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(WaitSeconds))
                .TakeUntilDestroy(data.Button)
                .Subscribe(_ => data.InputEvents.Execute());

                data.InputEvents.SetUp();
            });
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
