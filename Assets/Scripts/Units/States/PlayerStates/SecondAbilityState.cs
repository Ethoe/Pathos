using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAbilityState : BaseAbilityState
{
    private int moveParam = Animator.StringToHash("DashBlend"); // TODO: change to 'ability' animation
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
        player.statsComponent.stats.Invincible = true;
        player.TriggerAnimation(moveParam);
        startPos = player.transform.position;
        moveTarget = ((GetMouseLocation() - startPos).normalized * dashDistance) + startPos; // Get angle of mouse and multiply vector by dashdistance magnitude
        dashTimer = dashDistance / dashSpeed;
        deltaTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        player.statsComponent.stats.Invincible = false;
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
        return Vector2.zero;
    }
}
