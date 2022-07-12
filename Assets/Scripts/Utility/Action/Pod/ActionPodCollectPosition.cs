using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree.Execute;

public class ActionPodCollectPosition : Action
{
    [SerializeField] Vector3 _offsetPosition;
    Pod _pod;
    Transform _player;

    protected override void Setup(GameObject user)
    {
        _pod = user.GetComponent<Pod>();
        GameObject obj = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target;
        _player = obj.GetComponent<Player>().OffsetPosition;
    }

    protected override bool Execute()
    {
        _pod.transform.position = _player.position + _offsetPosition;

        return true;
    }

    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
