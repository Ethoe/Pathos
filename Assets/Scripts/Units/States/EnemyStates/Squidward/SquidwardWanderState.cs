using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidwardWanderState : SquidwardBaseState
{
    private Vector2 goingTo;
    private float wanderTime;

    public SquidwardWanderState(Squidward unit, StateMachine stateMachine) : base(unit, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        goingTo = new Vector2(unit.transform.position.x + Random.Range(-5, 5), unit.transform.position.y + Random.Range(-5, 5));
        RaycastHit2D hit = Physics2D.Raycast(goingTo, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Walls")); // Layer 9 is walls
        if (hit.collider != null)
        {
            goingTo = new Vector2(unit.transform.position.x + Random.Range(-5, 5), unit.transform.position.y + Random.Range(-5, 5));
        }
        wanderTime = 4.0f;
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
            if (Tools.percentChance(.5f)) // 70% chance to wander around again
            {
                stateMachine.ChangeState(unit.wander);
            }
            else
            {
                stateMachine.ChangeState(unit.idle);
            }
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
