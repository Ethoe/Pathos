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

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
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
