using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    /// <summary>
    /// And�ɑ�������
    /// </summary>

    public partial class TreeManager : MonoBehaviour
    {
        SequenceNode _sequenceNode;

        class SequenceNode
        {
            public int _blockID = 0;
            public int _queueID = 0;

            public void InitQueueID() => _queueID = 0;
            public void InitBrockID() => _blockID = 0;

            public int CurrentBrockID => _blockID;

            public BlockData SetBrockData(List<BlockData> brockDatas)
            {
                if (brockDatas.Count <= _blockID)
                {
                    return null;
                }

                return brockDatas[_blockID];
            }

            public QueueData SetQueueData(List<QueueData> queueDatas)
            {
                if (queueDatas.Count <= _queueID)
                {
                    return null;
                }
                return queueDatas[_queueID];
            }

            /// <summary>
            /// �u�����`�f�[�^���������߂邩�ǂ����̔���
            /// </summary>
            /// <param name="branch">���ׂ�u�����`�f�[�^</param>
            /// <returns>�������߂����ۂ�</returns>
            public bool SetNextBrockData(BranchData branch)
            {
                if (branch.BrockType == BrockType.ConditionallySelector)
                {
                    _blockID = 0;
                    return false;
                }

                _blockID++;

                if (branch.BrockDatas.Count <= _blockID)
                {
                    _blockID = 0;
                    return false;
                }

                return true;
            }

            public bool SetNextQueueData(BranchData branch)
            {
                if (branch.BrockDatas[_blockID].QueueType == QueueType.Selector)
                {
                    _queueID = 0;
                    return false;
                }

                _queueID++;
                
                if (branch.BrockDatas[_blockID].QueueDatas.Count <= _queueID)
                {
                    _queueID = 0;
                    return false;
                }
                
                return true;
            }

            public void Init()
            {
                _blockID = 0;
                _queueID = 0;
            }
        }
    }
} 
