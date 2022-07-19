using UnityEngine;
using UnityEditor;
using System;

namespace BehaviourTree.Edit
{
    public class DataBaseWindow : EditorWindow
    {
        static Action _closeAction;

        DataBaseGraphView _graphView;

        internal static void Open(string userName, Action closeAction)
        {
            _closeAction = closeAction;

            EditorWindow graphEditor = CreateInstance<DataBaseWindow>();
            graphEditor.Show();
            graphEditor.titleContent = new GUIContent($"{userName}_TreeDataBaseEditor");
        }

        private void OnEnable()
        {
            _graphView = new DataBaseGraphView();
            rootVisualElement.Add(_graphView);
        }

        private void OnDestroy()
        {
            _closeAction.Invoke();
        }
    }
}