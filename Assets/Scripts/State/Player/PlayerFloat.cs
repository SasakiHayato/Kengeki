using System;
using UnityEngine;

public class PlayerFloat : StateMachine.State
{
    float _timer;

    Player _player;
    JumpSetting _jumpSetting;
    PhysicsBase _physicsBase;

    const float ExitTime = 0.5f;

    public override void SetUp(GameObject user)
    {
        _player = user.GetComponent<Player>();
        _jumpSetting = user.GetComponent<JumpSetting>();
        _physicsBase = user.GetComponent<PhysicsBase>();
    }

    public override void Entry(string beforeStatePath)
    {
        _timer = 0;
        string animName = "";

        int id = _jumpSetting.CurrentID;

        if (id == 0) id = 1;

        switch (id)
        {
            case 1:
                animName = "Jump_Start";

                break;

            case 2:
                animName = "Double_Jump_Start_ZeroHeight";

                break;
        }

        _player.Anim.Play(animName);
        _player.CharaData.UpdateSpeed(_player.CharaData.DefaultSpeed);
    }

    public override void Run()
    {
        _player.Move((Vector2)GamePadInputter.Instance.GetValue(GamePadInputter.ValueType.PlayerMove));
        _timer += Time.deltaTime;
    }

    public override Enum Exit()
    {
        if (_physicsBase.IsGround && _timer > ExitTime) return Player.State.Idle;
        else return Player.State.Float;
    }
}
