using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovingState : BaseState
{
    private bool attack;
    private bool move;
    private bool idle;
    private int movingParam = Animator.StringToHash("WalkBlend");

    public AttackMovingState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(movingParam);
        attack = false;
        move = false;
        idle = false;
        ((PlayerStateMachine)stateMachine).WasAttackingState = true;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        move = moveAction.triggered;
        attack = attackAction.triggered;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (((PlayerStateMachine)stateMachine).target)
        {
            float distance = Vector2.Distance(player.transform.position, ((PlayerStateMachine)stateMachine).target.transform.position);
            if (distance < player.statsComponent.stats.AttackRange.Value)
            {
                attack = true;
            }
        }
        else
        {
            idle = true;
        }

        if (attack)
        {
            stateMachine.ChangeState(player.attacking);
        }
        else if (move)
        {
            stateMachine.ChangeState(player.moving);
        }
        else if (idle)
        {
            stateMachine.ChangeState(player.idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (((PlayerStateMachine)stateMachine).target)
        {
            player.PlayerMove(((PlayerStateMachine)stateMachine).target.transform.position);
        }
        else
        {
            idle = true;
        }
    }
}
