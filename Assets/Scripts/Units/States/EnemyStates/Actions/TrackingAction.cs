using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Track")]
public class TrackingAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.TimeInState -= Time.deltaTime;

        var movement = stateMachine.GetComponent<MovementController>();
        if (stateMachine.Target == null)
            return;

        movement.Target = stateMachine.Target.transform.position;
    }

    public override void Enter(BaseStateMachine stateMachine)
    {
        base.Enter(stateMachine);
        var animator = stateMachine.GetComponent<AnimationController>();
        if (animator != null)
            animator.TriggerAnimation(animator.animationParam["Move"]);
        stateMachine.Target = GameManager.Instance.player;
        stateMachine.TimeInState = Random.Range(4, 8);
    }

    public override void Exit(BaseStateMachine stateMachine)
    {
        var movement = stateMachine.GetComponent<MovementController>();
        movement.activelyMoving = false;
    }
}
