using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public enum EnemyPath
{
    CubeEnemySword,
    CubeEnemyBullet,
}

public enum EnemyType
{
    Mob,
    Boss,
}

[CreateAssetMenu (fileName = "EnemyDataTip")]
public class EnemyDataTip : ScriptableObject
{
    [SerializeField] EnemyType _enemyType;
    [SerializeField] List<DataTip> _dataTips;

    public int DataLegth => _dataTips.Count;

    public EnemyType EnemyType => _enemyType;
    public DataTip GetDataTip(int id) => _dataTips[id];
    
    [System.Serializable]
    public class DataTip
    {
        [SerializeField] List<EnemyPath> _enemyPaths;

        public List<EnemyPath> EnemyPaths => _enemyPaths;
    }
}
