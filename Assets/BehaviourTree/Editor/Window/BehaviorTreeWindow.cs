using UnityEngine;
using UnityEditor;
using System;

public class BehaviorTreeWindow : EditorWindow
{
    public static string WindowName => nameof(BehaviorTreeWindow);

    BehaviorTreeGraphView _graphView = null;
    static Action _action;

    public static void Open(string name, Action closeAction = null)
    {
        _action = closeAction;

        EditorWindow graphEditor = CreateInstance<BehaviorTreeWindow>(); 
        graphEditor.Show();
        graphEditor.titleContent = new GUIContent($"{name}_TreeEditor");
    }

    void OnEnable()
    {
        _graphView = new BehaviorTreeGraphView();

        rootVisualElement.Add(_graphView);
    }

    private void OnDestroy()
    {
        _action.Invoke();
    }
}
