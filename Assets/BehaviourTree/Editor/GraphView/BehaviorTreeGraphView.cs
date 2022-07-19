using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;


public class BehaviorTreeGraphView : GraphView
{
    SearchNodeGraphView _searchView;
    public BehaviorTreeGraphView() : base()
    {
        style.flexGrow = 1;
        style.flexShrink = 1;

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        Insert(0, new GridBackground());
        this.AddManipulator(new SelectionDragger());

        _searchView = new SearchNodeGraphView(Add);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        _searchView.BuildMenu(evt);
    }

    void Add(GraphElement element)
    {
        AddElement(element);
    }
}
