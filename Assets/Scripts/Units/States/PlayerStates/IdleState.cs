using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private int idleParam = Animator.StringToHash("IdleBlend");
    private bool attack;
    private bool move;
    public IdleState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }
    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(idleParam);
        attack = false;
        move = false;
        ((PlayerStateMachine)stateMachine).locked = false;
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
