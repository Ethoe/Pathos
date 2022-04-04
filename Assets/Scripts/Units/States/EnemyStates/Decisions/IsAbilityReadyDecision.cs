using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/IsAbilityReady")]
public class IsAbilityReadyDecision : Decision
{
    public override bool Decide(BaseStateMachine state)
    {
        var ability = state.GetComponent<AbilityHolder>();
        return ability.Abilities[AbilityClass.AbilityOne].state == AbilityState.ready;
    }
}
