using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();
    }

    public void ChangeState(State newState, GameObject param)
    {
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter(param);
    }
}
