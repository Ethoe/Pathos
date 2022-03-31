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
        if (attack)
        {
            EventManager.TriggerEvent(Events.PlayerEndTargettedTrigger, new Dictionary<string, object> { { "target", ((PlayerStateMachine)stateMachine).target } });
            ((PlayerStateMachine)stateMachine).target = null;
            stateMachine.ChangeState(player.attacking);
        }
        else if (move)
        {
            EventManager.TriggerEvent(Events.PlayerEndTargettedTrigger, new Dictionary<string, object> { { "target", ((PlayerStateMachine)stateMachine).target } });
            ((PlayerStateMachine)stateMachine).target = null;
            stateMachine.ChangeState(player.moving);
        }
        else if (idle)
        {
            EventManager.TriggerEvent(Events.PlayerEndTargettedTrigger, new Dictionary<string, object> { { "target", ((PlayerStateMachine)stateMachine).target } });
            ((PlayerStateMachine)stateMachine).target = null;
            stateMachine.ChangeState(player.idle);
        }

        if (((PlayerStateMachine)stateMachine).target != null)
        {
            float distance = Vector2.Distance(player.transform.position, ((PlayerStateMachine)stateMachine).target.transform.position);
            if (distance < player.statsComponent.stats.AttackRange.Value)
            {
                stateMachine.ChangeState(player.attacking);
                return;
            }
        }
        else
        {
            idle = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (((PlayerStateMachine)stateMachine).target != null)
        {
            player.PlayerMove(((PlayerStateMachine)stateMachine).target.transform.position);
        }
        else
        {
            idle = true;
        }
    }
}
