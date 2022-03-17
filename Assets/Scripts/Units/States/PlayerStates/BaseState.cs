using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : State
{
    protected PlayerController player;

    private bool second;
    public BaseState(PlayerController player, StateMachine stateMachine) : base(stateMachine)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        second = false;
    }

    public override void Enter(GameObject param)
    {
        base.Enter(param);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        second = Input.GetKeyDown(player.controls.SecondAbility);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (second)
        {
            stateMachine.ChangeState(player.secondAbility);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
