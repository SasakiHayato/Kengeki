using UnityEngine;
using BehaviourTree.Execute;

/// <summary>
/// BehaviorTreeの条件クラス。対象のObjectがCamera内に存在するかの成否
/// </summary>

public class ConditionInTheCm : BehaviourConditional
{
    float _viewAngle;
    Transform _user;

    protected override void Setup(GameObject user)
    {
        _viewAngle = Camera.main.fieldOfView;
        _user = user.transform;
    }

    protected override bool Try()
    {
        Vector3 dir = (_user.position - Camera.main.transform.position).normalized;
        float rad = Vector3.Dot(dir, Camera.main.transform.forward);

        float angle = Mathf.Acos(rad) * Mathf.Rad2Deg;

        return angle < _viewAngle;
    }

    protected override void Initialize()
    {
        _viewAngle = Camera.main.fieldOfView;
    }
}
