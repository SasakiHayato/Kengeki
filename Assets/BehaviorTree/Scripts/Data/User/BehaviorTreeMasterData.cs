using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// BehaviorTreeUserのマスターデータ
/// </summary>

namespace BehaviourTree.Data
{
    public class BehaviorTreeMasterData
    {
        public static BehaviorTreeMasterData Instance => s_instance;
        static BehaviorTreeMasterData s_instance = new BehaviorTreeMasterData();

        Dictionary<int, BehaviorTreeUserData> _userDic = new Dictionary<int, BehaviorTreeUserData>();

        int _id;

        const string UserPath = "BehaviorUser_No.";

        public static string CreateUserPath()
        {
            string path = UserPath + Instance._id.ToString();

            Instance._id++;

            return path;
        }

        public void CreateUser(int instanceID, string path, BehaviourTreeUser user, Transform offset)
        {
            BehaviorTreeUserData data = new BehaviorTreeUserData(user, offset, path);
            _userDic.Add(instanceID, data);
        }

        public BehaviorTreeUserData FindUserData(int instanceID)
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

        public BehaviorTreeUserData FindUserData(string path)
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

        public List<BehaviorTreeUserData> FindUserDataAll()
        {
            List<BehaviorTreeUserData> dataList = new List<BehaviorTreeUserData>();

            foreach (KeyValuePair<int, BehaviorTreeUserData> data in _userDic)
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
            Instance._userDic = new Dictionary<int, BehaviorTreeUserData>();
            Instance._id = 0;
        }
    }
}