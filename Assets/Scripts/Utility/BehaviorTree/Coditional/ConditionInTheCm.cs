using UnityEngine;
using BehaviourTree;

/// <summary>
/// BehaviorTreeの条件クラス。対象のObjectがCamera内に存在するかの成否
/// </summary>

public class ConditionInTheCm : IConditional
{
    float _viewAngle;
    Transform _user;

    public void SetUp(GameObject user)
    {
        _viewAngle = Camera.main.fieldOfView;
        _user = user.transform;
    }

    public bool Try()
    {
        Vector3 dir = (_user.position - Camera.main.transform.position).normalized;
        float rad = Vector3.Dot(dir, Camera.main.transform.forward);

        float angle = Mathf.Acos(rad) * Mathf.Rad2Deg;

        return angle < _viewAngle;
    }

    public void InitParam()
    {
        _viewAngle = Camera.main.fieldOfView;
    }
}
