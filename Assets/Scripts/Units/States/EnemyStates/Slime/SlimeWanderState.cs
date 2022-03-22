using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWanderState : SlimeBaseState
{
    private Vector2 goingTo;
    private float wanderTime;
    private int moveParam = Animator.StringToHash("MoveBlendTree");
    private WeightedChanceExecutor weightedChanceExecutor;

    public SlimeWanderState(Slime unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        weightedChanceExecutor = new WeightedChanceExecutor(
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.wander), 49),
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.track), 50),
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.idle), 1)
        );
    }
    public override void Enter()
    {
        base.Enter();
        goingTo = new Vector2(unit.transform.position.x + Random.Range(-5, 5), unit.transform.position.y + Random.Range(-5, 5));
        RaycastHit2D hit = Physics2D.Raycast(goingTo, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Walls"));
        if (hit.collider != null)
        {
            goingTo = new Vector2(unit.transform.position.x + Random.Range(-5, 5), unit.transform.position.y + Random.Range(-5, 5));
            hit = Physics2D.Raycast(goingTo, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Walls"));
        }
        unit.TriggerAnimation(moveParam);
        wanderTime = Random.Range(4, 8);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        wanderTime -= Time.deltaTime;
        if (Vector2.Distance(unit.transform.position, goingTo) <= 0.1 || wanderTime <= 0)
        {
            weightedChanceExecutor.Execute();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        unit.UnitMove(goingTo, unit.stats.MoveSpeed.Value);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
