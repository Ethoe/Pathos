using UnityEngine;
using System;

[CreateAssetMenu(menuName = "FSM/Actions/Stun")]
public class StunAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.TimeInState -= Time.deltaTime;
    }

    public override void Enter<T>(BaseStateMachine stateMachine, T param)
    {
        base.Enter(stateMachine);
        // var animator = stateMachine.GetComponent<AnimationController>();
        // if (animator != null)
        //     animator.TriggerAnimation(animator.animationParam["Idle"]);
        stateMachine.TimeInState = Convert.ToInt32(param);
    }
}
