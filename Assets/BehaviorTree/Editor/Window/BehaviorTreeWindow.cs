using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using BehaviourTree.Data;

public class BehaviorTreeWindow : EditorWindow
{
    public static string WindowName => nameof(BehaviorTreeWindow);

    BehaviorTreeGraphView _graphView = null;

    [OnOpenAsset(0)]
    public static bool Open(int instanceID, int line)
    {
        var asset = EditorUtility.InstanceIDToObject(instanceID);

        if (asset as TreeDataBase == null)
        {
            return false;
        } 

        EditorWindow graphEditor = CreateInstance<BehaviorTreeWindow>(); 
        graphEditor.Show();
        graphEditor.titleContent = new GUIContent($"{asset.name}_TreeEditor");

        return false;
    }

    void OnEnable()
    {
        _graphView = new BehaviorTreeGraphView();

        rootVisualElement.Add(_graphView);
    }
}
