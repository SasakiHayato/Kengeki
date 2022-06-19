using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class ActionPodCollectPosition : IAction
{
    [SerializeField] Vector3 _offsetPosition;
    Pod _pod;
    Transform _player;

    public void SetUp(GameObject user)
    {
        _pod = user.GetComponent<Pod>();
        GameObject obj = GameManager.Instance.FieldObject.GetData(ObjectType.GameUser)[0].Target;
        _player = obj.GetComponent<Player>().OffsetPosition;
    }

    public bool Execute()
    {
        _pod.transform.position = _player.position + _offsetPosition;

        return true;
    }

    public void InitParam()
    {
        
    }
}
