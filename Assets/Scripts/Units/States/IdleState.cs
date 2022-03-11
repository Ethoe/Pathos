using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private int idleParam = Animator.StringToHash("Idle");
    private bool attack;
    private bool move;
    public IdleState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(idleParam);
        attack = false;
        move = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        move = Input.GetKeyDown(KeyCode.Mouse1);
        attack = Input.GetKeyDown(KeyCode.Mouse0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (move)
        {
            stateMachine.ChangeState(player.moving);
        }
        else if (attack)
        {
            stateMachine.ChangeState(player.attacking);
        }
    }
}
