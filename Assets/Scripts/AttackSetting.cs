using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackSetting : MonoBehaviour
{
    [SerializeField] CharaBase _user;
    [SerializeField] List<AttackDataBase> _attackDatas;

    int _id = 0;
    AttackType _saveAttackType;
     
    public void Request(AttackType type)
    {
        AttackDataBase dataBase = _attackDatas.FirstOrDefault(d => d.AttackType == type);

        TypeCheck(dataBase, type);

        AttackDataBase.Data data = dataBase.GetData(_id);
        _user.Anim.Play(data.AnimName);

        _id++;
    }

    void TypeCheck(AttackDataBase dataBase, AttackType type)
    {
        if (dataBase.Length <= _id) _id = 0;

        if (_saveAttackType != type)
        {
            _saveAttackType = type;
            _id = 0;
        }
    }

    public void InitializeID() => _id = 0;
}
