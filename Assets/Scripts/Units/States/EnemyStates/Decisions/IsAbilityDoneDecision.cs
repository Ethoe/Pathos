using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/IsAbilityDone")]
public class IsAbilityDoneDecision : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        var ability = state.GetComponent<AbilityHolder>();
        return ability.Abilities[AbilityClass.AbilityOne].state == AbilityState.cooldown || ability.Abilities[AbilityClass.AbilityOne].state == AbilityState.ready;
    }
}
