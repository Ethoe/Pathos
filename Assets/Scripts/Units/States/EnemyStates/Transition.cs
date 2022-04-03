using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Transition")]
public class Transition : ScriptableObject
{
    public Decision Decision;
    public EnemyBaseState TrueState;
    public EnemyBaseState FalseState;

    public void Execute(BaseStateMachine stateMachine)
    {
        if (Decision.Decide(stateMachine) && !(TrueState is RemainInState))
            stateMachine.ChangeState(TrueState);
        else if (!(FalseState is RemainInState))
            stateMachine.ChangeState(FalseState);
    }
}
