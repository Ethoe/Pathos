using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{
    private bool attack;
    private Vector2 moveTarget;
    private int movingParam = Animator.StringToHash("WalkBlend");


    public MovingState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(movingParam);
        moveTarget = GetMouseLocation();
        attack = false;
        ((PlayerStateMachine)stateMachine).locked = false;
    }

    public override void Exit()
    {
        base.Exit();
        EventManager.TriggerEvent(Events.PlayerExitMove, null);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (moveAction.triggered || moveAction.ReadValue<float>() > 0f)
        {
            moveTarget = GetMouseLocation();
        }

        attack = attackAction.triggered;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        float distance = Vector2.Distance(player.transform.position, moveTarget);
        if (distance <= .1f)
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
        moveTarget = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
        EventManager.TriggerEvent(Events.PlayerClick, new Dictionary<string, object> { { "target", moveTarget } });
        return moveTarget + new Vector2(0, player.spriteRenderer.bounds.extents.y);
    }
}
