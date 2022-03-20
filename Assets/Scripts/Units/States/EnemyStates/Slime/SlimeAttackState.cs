using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : SlimeBaseState
{
    public SlimeAttackState(Slime unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        WeightedChanceExecutor weightedChanceExecutor = new WeightedChanceExecutor(
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.wander), 25)
        ); //25% chance (since 25 + 25 + 50 = 100)
    }
    public override void Enter()
    {
        base.Enter();
        unit.abilityTimer = unit.AbilityCooldown();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (unit.abilityTimer <= 0)
        {
            unit.Ability();
            unit.abilityTimer = unit.AbilityCooldown();
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
