using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public abstract class NodeBase : Node
{
    class PortController
    {
        const string InputName = "ì¸óÕ";
        const string OutPutName = "èoóÕ";

        public Port Input { get; private set; }
        public Port Output { get; private set; }

        System.Type a;

        public PortController()
        {
            Input = CreatePort(InputName, Direction.Input, Orientation.Horizontal, Port.Capacity.Single);
            Output = CreatePort(OutPutName, Direction.Output, Orientation.Horizontal, Port.Capacity.Multi);
        }

        private Port CreatePort(string name, Direction direction, Orientation orientation = Orientation.Horizontal, Port.Capacity capacity = Port.Capacity.Single)
        {
            var port = Port.Create<PortData>(orientation, direction, capacity, a);
            port.portName = name;

            return port;
        }
    }

    PortController _portController;

    public NodeBase()
    {
        _portController = new PortController();

        inputContainer.Add(_portController.Input);
        outputContainer.Add(_portController.Output);

        title = Path;
    }

    internal string Path 
    { 
        get
        {
            string path = SetPath();

            if (path == "")
            {
                path = "Node";
            }

            return path;
        }
    }

    protected virtual void Execute()
    {

    }

    protected abstract string SetPath();
}
