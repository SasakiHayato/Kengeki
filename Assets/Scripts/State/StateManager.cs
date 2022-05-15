using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace StateMachine
{
    /// <summary>
    /// StateMachine�̊Ǘ��N���X
    /// </summary>

    public class StateManager
    {
        bool _isRun = false;
        string _currentPath;
        
        GameObject _user;
        Dictionary<string, State> _stateDic;

        KeyValuePair<string, State> _currentKey;

        public StateManager(GameObject user)
        {
            _stateDic = new Dictionary<string, State>();
            _currentKey = default;
            
            _user = user;
            Debug.Log($"StateMachin. user : {user.name}");
        }

        /// <summary>
        /// �X�e�[�g�̒ǉ�
        /// </summary>
        /// <param name="state">�X�e�[�g</param>
        /// <param name="statePath">�p�X</param>
        /// <returns></returns>
        public StateManager AddState(State state, Enum statePath)
        {
            state.SetUp(_user);
            state.StateManager = this;
            _stateDic.Add(statePath.ToString(), state);
            Debug.Log($"AddState => {state}");

            return this;
        }

        /// <summary>
        /// Update�̐\��
        /// </summary>
        /// <param name="isRun">�\��</param>
        /// <param name="statePath">�X�e�[�g�p�X</param>
        /// <returns></returns>
        public void RunRequest(bool isRun, Enum statePath = null)
        {
            if (isRun && statePath != null)
            {
                _currentKey = _stateDic.FirstOrDefault(s => s.Key == statePath.ToString());
                _currentPath = statePath.ToString();

                _currentKey.Value.Entry();
            }

            _isRun = isRun;
        }

        /// <summary>
        /// Update
        /// </summary>
        public void Run()
        {
            if (!_isRun)
            {
                _currentKey = default;
                Debug.Log("StateMachine. Run is False");
                return;
            }

            Enum path = _currentKey.Value.Exit();

            if (path.ToString() != _currentPath)
            {
                ChangeState(path);
            }
            else
            {
                _currentKey.Value.Run();
            }
        }

        /// <summary>
        /// �X�e�[�g�̕ύX
        /// </summary>
        /// <param name="path">StatePath</param>
        public void ChangeState(Enum path, bool reEntry = false)
        {
            if (_currentKey.Key == path.ToString() && !reEntry)
            {
                Debug.Log($"Not change state. CurrentState => {_currentKey.Key}");
                return;
            }

            _currentKey = _stateDic.FirstOrDefault(s => s.Key == path.ToString());
            _currentPath = path.ToString();

            _currentKey.Value.Entry();
        }

        public Enum ExitChangeState(Enum path)
        {
            _currentKey = _stateDic.FirstOrDefault(s => s.Key == path.ToString());
            _currentPath = path.ToString();

            _currentKey.Value.Entry();

            return path;
        }
    }

    /// <summary>
    /// �e�X�e�[�g
    /// </summary>
    public abstract class State
    {
        public StateManager StateManager { get; set; }

        public abstract void SetUp(GameObject user);
        public abstract void Entry();
        public abstract void Run();
        public abstract Enum Exit();
    }
}
