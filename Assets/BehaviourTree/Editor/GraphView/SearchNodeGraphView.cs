using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class SearchNodeGraphView : GraphView
{
    public Action<GraphElement> ViewGraphAction { get; private set; }

    public SearchNodeGraphView(Action<GraphElement> action)
    {
        ViewGraphAction = action;
        SetEvent();
    }

    void SetEvent()
    {
        var searchNodeWindow = ScriptableObject.CreateInstance<SearchNodeModel>();
        searchNodeWindow.Initialize(this);

        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchNodeWindow);
        };
    }

    internal void BuildMenu(ContextualMenuPopulateEvent evt)
    {
        if (evt.target is GraphView && nodeCreationRequest != null)
        {
            evt.menu.AppendAction("Create Node", OnContextMenuNodeCreate, DropdownMenuAction.AlwaysEnabled);
            evt.menu.AppendSeparator();
        }
    }

    void OnContextMenuNodeCreate(DropdownMenuAction a)
    {
        RequestNodeCreation(null, -1, a.eventInfo.mousePosition);
    }

    void RequestNodeCreation(VisualElement target, int index, Vector2 position)
    {
        if (nodeCreationRequest == null)
            return;

        var graphEditorWindow = Resources.FindObjectsOfTypeAll<BehaviorTreeWindow>()
            .FirstOrDefault(window => window.GetType().Name == BehaviorTreeWindow.WindowName);

        Vector2 screenPoint = graphEditorWindow.position.position + position;

        nodeCreationRequest(new NodeCreationContext() { screenMousePosition = screenPoint, target = target, index = index });
    }
}
