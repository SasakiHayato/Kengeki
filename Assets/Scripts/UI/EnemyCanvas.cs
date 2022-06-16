using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 各Enemyに対するUIの表示クラス
/// </summary>

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] EnemyBase _user;
    [SerializeField] Slider _slider;

    Canvas _canvas;

    public void SetUp()
    {
        
        _canvas = GetComponent<Canvas>();

        _slider.maxValue = _user.CharaData.HP;

        _user.ObserveEveryValueChanged(u => u.CharaData.HP)
            .TakeUntilDestroy(_user)
            .Subscribe(_ => CanvasView());
    }

    void Update()
    {
        Vector3 forward = transform.position - Camera.main.transform.position;
        forward.y = 0;

        transform.rotation = Quaternion.LookRotation(forward.normalized);
    }

    void CanvasView()
    {
        _slider.maxValue = _user.CharaData.MaxHP;
        _slider.value = _user.CharaData.HP;

        if (_slider.maxValue <= _user.CharaData.HP) _canvas.enabled = false;
        else _canvas.enabled = true;
    }
}
