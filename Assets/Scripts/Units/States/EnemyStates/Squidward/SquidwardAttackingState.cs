using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidwardAttackingState : SquidwardBaseState
{
    public SquidwardAttackingState(Squidward unit, StateMachine stateMachine) : base(unit, stateMachine)
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
        base.LogicUpdate();
        if (unit.abilityHolder.Abilities[AbilityClass.AbilityOne].state == AbilityState.ready)
        {
            unit.abilityHolder.Activate(AbilityClass.AbilityOne, null, unit.aggrod.transform.position, unit.layer);
        }
        if (unit.abilityHolder.Abilities[AbilityClass.AbilityOne].state == AbilityState.cooldown)
        {
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
