using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidwardIdleState : SquidwardBaseState
{
    private float idleTime;
    public SquidwardIdleState(Squidward unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        idleTime = Random.Range(0, 3);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            if (Tools.percentChance(.7f))
            {
                stateMachine.ChangeState(unit.wander);
            }
            else
            {
                stateMachine.ChangeState(unit.idle);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
