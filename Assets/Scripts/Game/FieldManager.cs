using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// äeäKëwÇÃFieldÇÃä«óùÉNÉâÉX
/// </summary>

public class FieldManager : ManagerBase
{
    [SerializeField] MapCreater _creater;
    [SerializeField] bool _isSetPod;
    public List<RoomData> RoomList { get; private set; }

    float _timer = 0;
    bool _isCall = false;

    const float CallTime = 15f;
    const float SetYPosition = 5f;
    const float AddStatus = 0.5f;

    void Start()
    {
        GameManager.Instance.AddManager(this);
    }

    public override void SetUp()
    {
        _creater.Create();
        RoomList = _creater.RoomList;

        InstancePlayer();
        _creater.SetTeleportFrag();

        for (int i = 0; i < RoomList.Count; i++)
        {
            if (!RoomList[i].IsSetUp) InstanceEnemy(RoomList[i]);
        }

        base.SetUp();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > CallTime && !_isCall)
        {
            _isCall = true;
            object[] data = new object[] { GameManager.Instance.TextData.Request("SystemMSG", 2) };
            BaseUI.Instance.CallBack("GameUI", "Text", data);
        }
    }

    void InstancePlayer()
    {
        int randomID = Random.Range(0, RoomList.Count);
        GameObject player = Instantiate(GameManager.Instance.ObjectData.GetData("Player").Prefab);
 
        RoomData data = RoomList[randomID];
        Vector3 setPos = data.Position.Center;
        setPos.y = SetYPosition;
        player.transform.position = setPos;
       
        if (_isSetPod)
        {
            GameObject pod = Instantiate(GameManager.Instance.ObjectData.GetData("Pod").Prefab);
            pod.transform.position = setPos + player.transform.right;
        }

        data.Info.IsSetTeleporter(false);
        data.IsRoomSetUp(true);
    }

    void InstanceEnemy(RoomData data)
    {
        List<EnemyDataTip> dataList = GameManager.Instance.EnemyDataTipList;
        EnemyDataTip dataTip;
        
        if (_creater.IsSetArena)
        {
            dataTip = dataList.FirstOrDefault(d => d.EnemyType == EnemyType.Boss);
        }
        else
        {
            dataTip = dataList.FirstOrDefault(d => d.EnemyType == EnemyType.Mob);
        }

        int randomTip = Random.Range(0, dataTip.DataLegth);
        EnemyDataTip.DataTip tip = dataTip.GetDataTip(randomTip);

        foreach (EnemyPath path in tip.EnemyPaths)
        {
            GameObject obj = Instantiate(GameManager.Instance.ObjectData.GetData(path.ToString()).Prefab);

            float x = Random.Range(data.Position.UpperLeft.x, data.Position.BottomRight.x);
            float z = Random.Range(data.Position.UpperLeft.z, data.Position.BottomRight.z);

            Vector3 setPos = new Vector3(x, SetYPosition, z);
            obj.transform.position = setPos;

            EnemyBase enemyBase = obj.GetComponent<EnemyBase>();
            enemyBase.SetRoomID(data.Info.ID);
            enemyBase.SetItemPath(tip.ItemPath);

            data.Info.AddEnemy(enemyBase);
        }

        data.IsRoomSetUp(true);
    }

    public void AtInstanceEnemy(int roomID, EnemyPath path)
    {
        RoomData data = RoomList.FirstOrDefault(r => r.Info.ID == roomID);

        GameObject obj = Instantiate(GameManager.Instance.ObjectData.GetData(path.ToString()).Prefab);

        float x = Random.Range(data.Position.UpperLeft.x, data.Position.BottomRight.x);
        float z = Random.Range(data.Position.UpperLeft.z, data.Position.BottomRight.z);

        Vector3 setPos = new Vector3(x, SetYPosition, z);
        obj.transform.position = setPos;

        EnemyBase enemyBase = obj.GetComponent<EnemyBase>();
        enemyBase.SetRoomID(data.Info.ID);

        data.Info.AddEnemy(enemyBase);
    }

    public void RemoveEnemyEvent(int roomID, EnemyBase enemyBase)
    {
        RoomData data = RoomList.FirstOrDefault(r => r.Info.ID == roomID);
        data.Info.RemoveEnemy(enemyBase);
        
        if (data.Info.SetTeleporter && data.Info.EnemyList.Count() <= 0)
        {
            _creater.SetTeleport();
            _isCall = true;
            object[] info = new object[] { GameManager.Instance.TextData.Request("SystemMSG", 4) };
            BaseUI.Instance.CallBack("GameUI", "Text", info);
        }
    }

    public void UpdateStatus(CharaBase chara)
    {
        int hierarchy = GameManager.Instance.FieldHierarchy;

        CharaData data = chara.CharaData;

        if (hierarchy <= 1) return;

        float add = hierarchy * AddStatus;
        
        data.UpdateMaxHP(data.MaxHP + (int)add, true);
        data.UpdatePower(data.Power + (int)add);
    }

    public RoomData GetRoomData(int id) => RoomList.FirstOrDefault(r => r.Info.ID == id);

    public override GameObject ManagerObject() => gameObject;
    public override string ManagerPath() => nameof(FieldManager);
}
