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
        var animator = stateMachine.GetComponent<AnimationController>();
        if (abilityHolder.Abilities[AbilityClass.AbilityOne].state == AbilityState.ready)
        {
            if (animator != null)
                animator.TriggerAnimation(animator.animationParam["Ability"]);
            abilityHolder.Activate(AbilityClass.AbilityOne, stateMachine.Target, stateMachine.Target.transform.position, stateMachine.layer);
        }
    }

    public override void Exit(BaseStateMachine stateMachine)
    {

    }
}
