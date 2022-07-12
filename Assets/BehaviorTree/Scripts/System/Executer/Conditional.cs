using UnityEngine;

namespace BehaviourTree.Execute
{
    /// <summary>
    /// Conditionを作成する際の基底クラス
    /// このクラスを派生してAIの行動条件の作成をする。
    /// </summary>
    [System.Serializable]
    public abstract class Conditional : ExecuteBase
    {
        public override void BaseInit() => Initialize();

        public override void BaseSetup(GameObject user) => Setup(user);

        protected abstract void Setup(GameObject user);

        protected abstract bool Try();

        protected override bool BaseExecute() => Try();

        protected abstract void Initialize();
    }
}

