using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidwardAttackingState : SquidwardBaseState
{
    private float timer;
    public SquidwardAttackingState(Squidward unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        timer = 1.0f;
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            unit.Ability(unit.aggrod.transform.position - unit.transform.position);
            timer = 1;
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
