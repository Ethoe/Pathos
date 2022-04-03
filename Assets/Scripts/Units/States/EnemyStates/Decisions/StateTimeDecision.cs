using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/TimeOut")]
public class StateTimeDecision : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        return state.TimeInState < 0;
    }
}
