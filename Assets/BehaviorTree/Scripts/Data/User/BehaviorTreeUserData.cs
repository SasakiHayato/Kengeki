using UnityEngine;

namespace BehaviourTree.Data
{
    /// <summary>
    /// BehaviorTreeUserのデータクラス
    /// </summary>
    public class BehaviorTreeUserData
    {
        public BehaviorTreeUserData(BehaviourTreeUser user, Transform transform, string path)
        {
            Offset = transform;
            User = user;
            Path = path;
        }

        public string Path { get; private set; }

        public Transform Offset { get; private set; }

        public BehaviourTreeUser User { get; private set; }
        
        public int LimitConditionalCount { get; private set; }

        public void SetLimitConditionalCount(int count) => LimitConditionalCount = count;
        
        public bool IsLimitCondition() => LimitConditionalCount > 0;
    }
}
