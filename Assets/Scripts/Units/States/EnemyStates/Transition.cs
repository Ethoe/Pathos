using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StateWeight
{
    public EnemyBaseState State;
    public float Weight;
}

[CreateAssetMenu(menuName = "FSM/Transition")]
public class Transition : ScriptableObject
{
    public Decision Decision;
    public List<StateWeight> TrueState = new List<StateWeight>();
    public EnemyBaseState FalseState;

    public void Execute(BaseStateMachine stateMachine)
    {
        if (Decision.Decide(stateMachine) && TrueState.Count > 0)
        {
            WeightedChanceExecutor weightedChanceExecutor = new WeightedChanceExecutor();
            foreach (var state in TrueState)
            {
                weightedChanceExecutor.AddChance(new WeightedChanceParam(() => { stateMachine.ChangeState(state.State); }, state.Weight));
            }
            weightedChanceExecutor.Execute();
        }
        else if (!(FalseState is RemainInState))
        {
            stateMachine.ChangeState(FalseState);
        }
    }
}
