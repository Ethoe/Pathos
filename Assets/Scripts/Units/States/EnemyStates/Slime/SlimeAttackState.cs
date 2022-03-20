using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : SlimeBaseState
{
    private int abilityParam = Animator.StringToHash("AbilityBlendTree");
    private float abilityLength;
    private bool casted;
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
        abilityLength = .35f; //Animation length
        casted = false;
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
            unit.TriggerAnimation(abilityParam);
            casted = true;
            unit.Ability();
            unit.abilityTimer = unit.AbilityCooldown();
        }
        if (casted)
        {
            abilityLength -= Time.deltaTime;
            if (abilityLength <= 0)
            {
                weightedChanceExecutor.Execute();
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
