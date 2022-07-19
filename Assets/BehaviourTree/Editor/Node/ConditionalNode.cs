using UnityEngine;
using System.Collections.Generic;
using BehaviourTree.Execute;

public class ConditionalNode : NodeBase
{
    [SerializeReference, SubclassSelector]
    List<BehaviourConditional> _list;
    public ConditionalNode() : base() 
    {
        
        //mainContainer.Add(new PropertyField(_list));
        RefreshExpandedState();
    }

    protected override string SetPath() => "Condition";
}
