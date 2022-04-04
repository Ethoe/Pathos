using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/IsInRange")]
public class IsInRangeDecision : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        var ability = state.GetComponent<AbilityHolder>();
        if (state.Target == null)
            return false;
        return (Vector2.Distance(state.Target.transform.position, state.gameObject.transform.position) <= ability.Abilities[AbilityClass.AbilityOne].Ability.Range);
    }
}
