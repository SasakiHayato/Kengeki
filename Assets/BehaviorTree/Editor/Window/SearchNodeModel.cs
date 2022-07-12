using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;

public class SearchNodeModel : ScriptableObject, ISearchWindowProvider
{
    SearchNodeGraphView _graph;
    public void Initialize(SearchNodeGraphView s)
    {
        _graph = s;
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> list = new List<SearchTreeEntry>();
        list.Add(new SearchTreeGroupEntry(new GUIContent("Node")));

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass && type.IsSubclassOf(typeof(NodeBase)))
                {
                    NodeBase node = Activator.CreateInstance(type) as NodeBase;
                    list.Add(new SearchTreeEntry(new GUIContent(node.Path)) { level = 1, userData = type });
                }
            }
        }

        return list;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var graphEditorWindow = Resources.FindObjectsOfTypeAll<BehaviorTreeWindow>()
            .FirstOrDefault(window => window.GetType().Name == BehaviorTreeWindow.WindowName);

        var type = SearchTreeEntry.userData as Type;
        NodeBase node = Activator.CreateInstance(type) as NodeBase;
        _graph.ViewGraphAction.Invoke(node);

        return true;
    }
}
