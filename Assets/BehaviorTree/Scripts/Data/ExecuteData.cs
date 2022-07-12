using UnityEngine;
using BehaviourTree.Node;

namespace BehaviourTree.Data
{
    /// <summary>
    /// AI�s���̃f�[�^�N���X
    /// </summary>
    [System.Serializable]
    public class ExecuteData
    {
        [SerializeField] ExecuteType _treeExecuteType;
        [SerializeField] ConditionalNode _condition;
        [SerializeField] ActionNode _action;

        public ExecuteType TreeExecuteType => _treeExecuteType;
        public ConditionalNode Condition => _condition;
        public ActionNode Action => _action;
    }
}