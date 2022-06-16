using UnityEngine;
using System.Collections.Generic;

public enum EnemyPath
{
    CubeEnemySword,
    CubeEnemyBullet,

    BossGrruzam,
}

public enum EnemyType
{
    Mob,
    Boss,
}

/// <summary>
/// Enemyの各Roomに対する生成データ
/// </summary>

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
        [SerializeField] List<string> _itemPathList;
        [SerializeField] List<EnemyPath> _enemyPaths;

        public string ItemPath
        {
            get
            {
                int random = Random.Range(0, _itemPathList.Count);
                return _itemPathList[random];
            }
        }
        public List<EnemyPath> EnemyPaths => _enemyPaths;
    }
}
