using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMovingState : BaseState
{
    private bool attack;
    private bool move;
    private bool idle;
    private GameObject target;
    private int movingParam = Animator.StringToHash("Move");

    public AttackMovingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter(GameObject param)
    {
        base.Enter(param);
        target = param;
        player.TriggerAnimation(movingParam);
        attack = false;
        move = false;
        idle = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        move = Input.GetKeyDown(player.controls.Move);
        attack = Input.GetKeyDown(player.controls.AttackMove);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (target)
        {
            float distance = Vector2.Distance(player.transform.position, target.transform.position);
            if (distance < player.stats.AttackRange.Value)
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
            stateMachine.ChangeState(player.attacking, target);
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
        if (target)
        {
            player.PlayerMove(target.transform.position);
        }
        else
        {
            idle = true;
        }
    }
}
