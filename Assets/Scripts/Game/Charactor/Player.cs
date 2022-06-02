using UnityEngine;
using StateMachine;

/// <summary>
/// PlayerŠÇ—ƒNƒ‰ƒX
/// </summary>

public class Player : CharaBase, IDamage
{
    public enum State
    {
        Idle,
        Move,
        Float,
        Attack,
        Dodge,
    }

    Vector3 _saveDir;

    JumpSetting _jumpSetting;
    StateManager _state;

    bool _isDodge = false;
    public bool IsDodge
    {
        get
        {
            bool isDodge = _isDodge;
            _isDodge = false;
            return isDodge;
        }
    }

    protected override void SetUp()
    {
        base.SetUp();

        _jumpSetting = GetComponent<JumpSetting>();

        GamePadInputter.Instance.Input.Player.Dodge.performed += context => Dodge();
        GamePadInputter.Instance.Input.Player.Jump.performed += context => Jump();
        GamePadInputter.Instance.Input.Player.Attack.performed += context => Attack();
        GamePadInputter.Instance.Input.Player.Lockon.performed += context => Lockon();
        
        _state = new StateManager(gameObject);
        _state.AddState(new PlayerIdle(), State.Idle)
            .AddState(new PlayerMove(), State.Move)
            .AddState(new PlayerFloat(), State.Float)
            .AddState(new PlayerAttack(), State.Attack)
            .AddState(new PlayerDodge(), State.Dodge)
            .RunRequest(true, State.Idle);

        BaseUI.Instance.CallBack("PlayerUI", "HP");
        GameManager.Instance.GetManager<CmManager>(nameof(CmManager)).SetUser();
    }

    void Update()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.Player) return;

        _state.Run();
    }

    /// <summary>
    /// PlayerState‚©‚ç‚ÌˆÚ“®§Œä
    /// </summary>
    /// <param name="input">“ü—Í•ûŒü</param>
    public void Move(Vector2 input)
    {
        Vector3 forward = Camera.main.transform.forward * input.y;
        Vector3 right = Camera.main.transform.right * input.x;

        Vector3 move = (forward + right) * CharaData.Speed;
        move.y = 1 + _jumpSetting.Power * -1;

        RB.velocity = Vector3.Scale(move, PhysicsBase.Gravity);
    }

    /// <summary>
    /// PlayerState‚©‚ç‚Ì‰ñ“]§Œä
    /// </summary>
    public void Rotate(Vector3 dir)
    {
        if (dir == Vector3.zero) dir = _saveDir;
        
        dir.y = 0;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            _saveDir = dir;
        }
    }

    public void InitDodgeCheck() => _isDodge = false;

    void Lockon()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.Player) return;

        if (GameManager.Instance.LockonTarget != null)
        {
            GameManager.Instance.LockonTarget = null;
        }
        else
        {
            CmManager cmManager = GameManager.Instance.GetManager<CmManager>(nameof(CmManager));
            GameManager.Instance.LockonTarget = cmManager.FindCenterTarget(ObjectType.Enemy, 50).transform;
        }
    }

    void Jump()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.Player) return;

        if (PhysicsBase.IsGround) _jumpSetting.Init();

        _jumpSetting.Set();

        if (_jumpSetting.IsSet)
        {
            _state.ChangeState(State.Float, true);
            PhysicsBase.InitializeTumer();
        }
    }

    void Attack()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.Player) return;

        _state.ChangeState(State.Attack, true);
    }

    void Dodge()
    {
        if (GamePadInputter.Instance.CurrentInputterType != InputterType.Player) return;
        _state.ChangeState(State.Dodge);
    }

    // IDamage
    public bool GetDamage(int damage)
    {
        if (_state.CurrentPathName == State.Dodge.ToString())
        {
            _isDodge = true;
            Effects.Instance.RequestDodgeEffect();

            return false;
        }

        if (damage <= 0) return false;

        CharaData.UpDateHP(-damage);
        return true;
    }
}
