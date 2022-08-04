using UnityEngine;
using BehaviourTree.Data;
using BehaviourTree.IO;

namespace BehaviourTree
{
    /// <summary>
    /// ゲーム開始と終わりのBehaviourTreeの初期化
    /// </summary>
    
    public class BehaviourTreeSetting : MonoBehaviour
    {
        void Awake()
        {
            BehaviourTreeIO.Initialize();
        }

        void OnDestroy()
        {
            BehaviourTreeIO.Update();
            BehaviourTreeMasterData.Dispose();
        }
    }
}