using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBaseState : State
{
    protected Slime unit;
    public SlimeBaseState(Slime unit, StateMachine stateMachine) : base(stateMachine)
    {
        this.unit = unit;
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
