using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbilityState : State
{
    protected PlayerController player;

    private State buffer;

    public BaseAbilityState(PlayerController player, StateMachine stateMachine) : base(stateMachine)
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        buffer = player.idle;
    }

    public override void Enter(GameObject param)
    {
        base.Enter(param);
        buffer = player.idle;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(player.controls.Move))
            buffer = player.moving;
        else if (Input.GetKeyDown(player.controls.AttackMove))
            buffer = player.attacking;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void End()
    {
        stateMachine.ChangeState(buffer);
    }
}
