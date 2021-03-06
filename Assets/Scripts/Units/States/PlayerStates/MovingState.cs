using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{
    private bool attack;
    private int movingParam = Animator.StringToHash("WalkBlend");

    private float ReinputBuffer;


    public MovingState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        ReinputBuffer = .1f;
    }
    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(movingParam);
        GetMouseLocation();
        ReinputBuffer = .1f;
        attack = false;
        ((PlayerStateMachine)stateMachine).locked = false;
    }

    public override void Exit()
    {
        base.Exit();
        player.movement.activelyMoving = false;
        EventManager.TriggerEvent(Events.PlayerExitMove, null);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if ((moveAction.triggered || moveAction.ReadValue<float>() > 0f) && ReinputBuffer <= 0)
        {
            ReinputBuffer = .1f;
            GetMouseLocation();
        }

        attack = attackAction.triggered;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        ReinputBuffer -= Time.deltaTime;

        if (!player.movement.IsMoving)
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
    }

    private void GetMouseLocation()
    {
        Vector2 moveTarget = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
        EventManager.TriggerEvent(Events.PlayerClick, new Dictionary<string, object> { { "target", moveTarget } });
        moveTarget += new Vector2(0, player.spriteRenderer.bounds.extents.y);
        player.movement.Target = moveTarget;
    }
}
