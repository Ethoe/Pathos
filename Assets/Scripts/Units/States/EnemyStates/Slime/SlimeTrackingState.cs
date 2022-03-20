using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrackingState : SlimeBaseState
{
    private Vector2 goingTo;
    private float trackingTime;
    private WeightedChanceExecutor weightedChanceExecutor;
    private int moveParam = Animator.StringToHash("MoveBlendTree");

    public SlimeTrackingState(Slime unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
        weightedChanceExecutor = new WeightedChanceExecutor(
            new WeightedChanceParam(() => stateMachine.ChangeState(unit.attack), 100)
        );
    }
    public override void Enter()
    {
        base.Enter();
        trackingTime = Random.Range(5, 10);
        unit.TriggerAnimation(moveParam);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        trackingTime -= Time.deltaTime;
        if (Vector2.Distance(unit.transform.position, GameManager.Instance.player.transform.position) <= 0.3 || trackingTime <= 0)
        {
            weightedChanceExecutor.Execute();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        unit.UnitMove(GameManager.Instance.player.transform.position, unit.stats.MoveSpeed.Value);
    }

    public override void Exit()
    {
        base.Exit();
    }
}