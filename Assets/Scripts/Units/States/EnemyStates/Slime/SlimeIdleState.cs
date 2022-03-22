using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeBaseState
{
    private float idleTime;
    private int idleParam = Animator.StringToHash("Idle");
    private WeightedChanceExecutor weightedChanceExecutor;

    public SlimeIdleState(Slime unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        weightedChanceExecutor = new WeightedChanceExecutor(
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.attack), 50),
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.wander), 50)
        );
    }
    public override void Enter()
    {
        base.Enter();
        unit.TriggerAnimation(idleParam);
        idleTime = Random.Range(4, 8);
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
            weightedChanceExecutor.Execute();
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
