using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{
    private bool attack;
    private Vector2 moveTarget;
    private int movingParam = Animator.StringToHash("WalkBlend");


    public MovingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(movingParam);
        moveTarget = GetMouseLocation();
        attack = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetKeyDown(player.controls.Move))
        {
            moveTarget = GetMouseLocation();
        }

        attack = Input.GetKeyDown(player.controls.AttackMove);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float distance = Vector2.Distance(player.transform.position, moveTarget);
        if (Mathf.Approximately(distance, 0))
        {
            stateMachine.ChangeState(player.idle);
        }
        else if (attack)
        {
            stateMachine.ChangeState(player.attacking);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.PlayerMove(moveTarget);
    }

    private Vector2 GetMouseLocation()
    {
        moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return moveTarget;
    }
}
