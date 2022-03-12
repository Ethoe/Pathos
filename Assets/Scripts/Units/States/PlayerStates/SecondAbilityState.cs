using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAbilityState : BaseAbilityState
{
    private int moveParam = Animator.StringToHash("Move"); // TODO: change to 'ability' animation
    private float dashTime = .5f;
    private float dashTimer;
    private float dashSpeed = 15.0f;
    private float dashDistance = 3.0f;
    private float deltaTime;
    private Vector2 startPos;
    private Vector2 moveTarget;
    public SecondAbilityState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.stats.Invincible = true;
        player.TriggerAnimation(moveParam);
        startPos = player.transform.position;
        moveTarget = ((GetMouseLocation() - startPos).normalized * dashDistance) + startPos; // Get angle of mouse and multiply vector by dashdistance magnitude
        Debug.Log((GetMouseLocation() - startPos).normalized);
        dashTimer = dashTime;
        deltaTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        player.stats.Invincible = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float distance = Vector2.Distance(player.transform.position, startPos);
        if (distance >= dashDistance)
        {
            base.End();
        }

        dashTimer -= Time.time - deltaTime;
        deltaTime = Time.time;
        if (dashTimer < 0)
        {
            base.End();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.PlayerMove(moveTarget, dashSpeed);
    }

    private Vector2 GetMouseLocation()
    {
        moveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return moveTarget;
    }
}
