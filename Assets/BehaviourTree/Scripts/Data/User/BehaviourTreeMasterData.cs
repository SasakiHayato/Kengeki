using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// BehaviorTreeUserのマスターデータ
/// </summary>

namespace BehaviourTree.Data
{
    public class BehaviourTreeMasterData
    {
        public static BehaviourTreeMasterData Instance => s_instance;
        static BehaviourTreeMasterData s_instance = new BehaviourTreeMasterData();

        Dictionary<int, BehaviourTreeUserData> _userDic = new Dictionary<int, BehaviourTreeUserData>();

        int _pathID;
        int _userID;

        const string UserPath = "BehaviorUser_No.";

        public static string CreateUserPath()
        {
            string path = UserPath + Instance._pathID.ToString();

            Instance._pathID++;

            return path;
        }

        public static int CreateUserID()
        {
            int id = Instance._userID;

            Instance._userID++;

            return id;
        }

        public void CreateUser(int instanceID, string path, BehaviourTreeUser user, Transform offset)
        {
            BehaviourTreeUserData data = new BehaviourTreeUserData(user, offset, path);
            _userDic.Add(instanceID, data);
        }

        public BehaviourTreeUserData FindUserData(int instanceID)
        {
            try
            {
                return _userDic.First(u => u.Key == instanceID).Value;
            }
            catch (Exception)
            {
                Debug.Log($"NothingData. FindID{instanceID}.  Return => Null");
                return null;
            }
        }

        public BehaviourTreeUserData FindUserData(string path)
        {
            try
            {
                return _userDic.First(u => u.Value.Path == path).Value;
            }
            catch (Exception)
            {
                Debug.Log($"NothingData. FindPath{path}.  Return => Null");
                return null;
            }
        }

        public List<BehaviourTreeUserData> FindUserDataAll()
        {
            List<BehaviourTreeUserData> dataList = new List<BehaviourTreeUserData>();

            foreach (KeyValuePair<int, BehaviourTreeUserData> data in _userDic)
            {
                dataList.Add(data.Value);
            }

            return dataList;
        }

        public void DeleteUser(int instanceID)
        {
            if (_userDic.Count <= 0)
            {
                return;
            }

            _userDic.Remove(instanceID);
        }

        public static void Dispose()
        {
            Instance._userDic = new Dictionary<int, BehaviourTreeUserData>();
            Instance._pathID = 0;
            Instance._userID = 0;
        }
    }
}