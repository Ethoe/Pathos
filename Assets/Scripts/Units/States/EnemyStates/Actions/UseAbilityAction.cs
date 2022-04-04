using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/UseAbility")]
public class UseAbilityAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {

    }

    public override void Enter(BaseStateMachine stateMachine)
    {
        base.Enter(stateMachine);
        var abilityHolder = stateMachine.GetComponent<AbilityHolder>();
        if (abilityHolder.Abilities[AbilityClass.AbilityOne].state == AbilityState.ready)
        {
            // TriggerAnimation(abilityParam); // make event trigger
            abilityHolder.Activate(AbilityClass.AbilityOne, stateMachine.Target, stateMachine.Target.transform.position, stateMachine.layer);
        }
    }

    public override void Exit(BaseStateMachine stateMachine)
    {

    }
}
