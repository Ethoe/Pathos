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
        stateMachine.TimeInState = Random.Range(4, 8);
    }
}
