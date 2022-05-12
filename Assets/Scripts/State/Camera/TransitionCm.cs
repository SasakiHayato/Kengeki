using System;
using UnityEngine;

public class TransitionCm : StateMachine.State
{
    public override void SetUp(GameObject user)
    {

    }

    public override void Entry()
    {
        
    }

    public override void Run()
    {
        
    }

    public override Enum Exit()
    {
        return CmManager.State.Transition;
    }
}
