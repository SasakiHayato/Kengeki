using System.Collections.Generic;
using System;
using UnityEngine;
using BehaviourTree;
using BehaviourTree.Data;

/// <summary>
/// BehaviorTree���g�p����Object�ɃA�^�b�`����N���X
/// AI�����̑�����s��
/// </summary>

public class BehaviourTreeUser : MonoBehaviour
{
    [SerializeField] string _path;
    [SerializeField] bool _runUpdate = true;
    [SerializeField] Transform _offset;
    [SerializeField] int _limitConditionalCount;
    [SerializeField] List<TreeDataBase> _treeDataList;

    TreeModel _treeModel;
    ModelData ModelData => _treeModel.ModelData;

    bool _runRequest;

    /// <summary>
    /// �C�ӂ̃^�C�~���O��TreeModel���Ăяo��
    /// </summary>
    public Action OnNext { get; private set; }
    
    void Start()
    {
        SetData();

        _treeDataList
            .ForEach(d => d.NodeList
            .ForEach(n => 
            {
                n.SetUp();
                n.SetNodeUser(gameObject);
            }));

        _treeModel = new TreeModel(_treeDataList);

        OnNext += () => 
        {
            if (ModelData.TreeDataBase == null ||
            !ModelData.TreeDataBase.IsAccess ||
            ModelData.ExecuteData == null)
            {
                Set();
            }
        };

        OnNext += () =>
        {
            if (Execute())
            {
                switch (ModelData.TreeData.TreeType)
                {
                    case ConditionType.Selector: Set(); break;
                    case ConditionType.Sequence: _treeModel.OnNext(); break;
                }
            }
        };
    }

    void SetData()
    {
        Transform offset = _offset;

        if (offset == null)
        {
            offset = transform;
        }

        if (_path == "")
        {
            _path = BehaviorTreeMasterData.CreateUserPath();
            Debug.LogWarning($"{gameObject.name} has not UserPath. So Create it. PathName is => {_path}.");
        }

        BehaviorTreeMasterData.Instance.CreateUser(GetInstanceID(), _path, this, offset);
        BehaviorTreeMasterData.Instance
            .FindUserData(GetInstanceID())
            .SetLimitConditionalCount(_limitConditionalCount);

    }

    void Update()
    {
        if (_runUpdate && _runRequest)
        {
            Run();
        }
    }

    void Run()
    {
        if (ModelData.TreeDataBase == null ||
            !ModelData.TreeDataBase.IsAccess ||
            ModelData.ExecuteData == null)
        {
            Set();
        }
        else
        {
            if (Execute())
            {
                switch (ModelData.TreeData.TreeType)
                {
                    case ConditionType.Selector: Set(); break;
                    case ConditionType.Sequence: _treeModel.OnNext(); break;
                }
            }
        }
    }

    /// <summary>
    /// TreeData�̑}��
    /// </summary>
    void Set()
    {
        _treeModel.Init(ModelData.TreeDataBase);
        _treeModel.OnNext();
    }

    /// <summary>
    /// TreeData�̎��sProcess
    /// </summary>
    bool Execute()
    {
        if (_treeModel.CheckIsCondition(ModelData.ExecuteData))
        {
            // Note. �s�����S�ďI���������_��True��Ԃ�
            if (ModelData.ExecuteData.Action.IsProcess)
            {
                return true;
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    public void SetRunRequest(bool isRun) => _runRequest = isRun;

    private void OnDestroy()
    {
        BehaviorTreeMasterData.Instance.DeleteUser(GetInstanceID());
    }
}
