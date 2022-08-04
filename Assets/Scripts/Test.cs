using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;
using BehaviourTree;

public interface ITest
{
    void AA();
}

public class Test : MonoBehaviour
{
    //[SerializeReference, SubclassSelector]
    //List<ExecuteBase> AA;

    [SerializeReference, SubclassSelector]
    List<BehaviourConditional> _AA;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
