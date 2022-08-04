using UnityEngine;
using BehaviourTree.Data;
using BehaviourTree.IO;

namespace BehaviourTree
{
    /// <summary>
    /// �Q�[���J�n�ƏI����BehaviourTree�̏�����
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