using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidwardWanderState : SquidwardBaseState
{
    public SquidwardWanderState(Squidward unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        unit.UnitMove(new Vector2(unit.transform.position.x + Random.Range(-5, 5), unit.transform.position.y + Random.Range(-5, 5)), unit.stats.MoveSpeed.Value);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
