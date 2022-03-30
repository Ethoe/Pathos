using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : SlimeBaseState
{
    private int abilityParam = Animator.StringToHash("AbilityBlendTree");
    private WeightedChanceExecutor weightedChanceExecutor;
    public SlimeAttackState(Slime unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        weightedChanceExecutor = new WeightedChanceExecutor(
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.wander), 50),
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.track), 50)
        ); //25% chance (since 25 + 25 + 50 = 100)
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
        if (unit.abilityHolder.Abilities[AbilityClass.AbiltyOne].state == AbilityState.ready)
        {
            unit.TriggerAnimation(abilityParam);
            unit.abilityHolder.Activate(AbilityClass.AbiltyOne, null, unit.transform.position, unit.layer);
        }
        if (unit.abilityHolder.Abilities[AbilityClass.AbiltyOne].state == AbilityState.cooldown)
        {
            weightedChanceExecutor.Execute();
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
