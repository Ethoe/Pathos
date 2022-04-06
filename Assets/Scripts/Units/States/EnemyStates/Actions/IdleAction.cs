using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Idle")]
public class IdleAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.TimeInState -= Time.deltaTime;
    }

    public override void Enter(BaseStateMachine stateMachine)
    {
        base.Enter(stateMachine);
        var animator = stateMachine.GetComponent<AnimationController>();
        if (animator != null)
            animator.TriggerAnimation(animator.animationParam["Idle"]);
        stateMachine.TimeInState = Random.Range(4, 8);
    }
}
