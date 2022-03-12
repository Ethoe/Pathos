using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAbilityState : BaseAbilityState
{
    private int idleParam = Animator.StringToHash("Idle"); // TODO: change to 'ability' animation
    private bool attack;
    private bool move;
    private bool idle;
    private GameObject ability;
    public FirstAbilityState(PlayerController player, StateMachine stateMachine, GameObject ability) : base(player, stateMachine)
    {
        this.ability = ability;
    }

    public override void Enter()
    {
        base.Enter();
        player.TriggerAnimation(idleParam);
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
