using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : State
{
    protected float speed;
    protected float rotationSpeed;

    private float horizontalInput;
    private float verticalInput;

    public BaseState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
