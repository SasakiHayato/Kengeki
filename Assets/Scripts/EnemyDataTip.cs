using UnityEngine;
using System.Collections.Generic;

public enum EnemyPath
{
    CubeEnemy,
}

[CreateAssetMenu (fileName = "EnemyDataTip")]
public class EnemyDataTip : ScriptableObject
{
    [SerializeField] List<DataTip> _dataTips;

    public int DataLegth => _dataTips.Count;
    public DataTip GetDataTip(int id) => _dataTips[id];

    [System.Serializable]
    public class DataTip
    {
        [SerializeField] List<EnemyPath> _enemyPaths;

        public List<EnemyPath> EnemyPaths => _enemyPaths;
    }
}
