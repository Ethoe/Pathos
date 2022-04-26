using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/EnemyState")]
public class EnemyState : EnemyBaseState
{
    public List<FSMAction> Action = new List<FSMAction>();
    public List<Transition> Transitions = new List<Transition>();

    public override void Execute(BaseStateMachine machine)
    {
        foreach (var action in Action)
        {
            action.Execute(machine);
        }

        foreach (var transition in Transitions)
        {
            transition.Execute(machine);
        }
    }

    public override void Enter(BaseStateMachine machine)
    {
        foreach (var action in Action)
        {
            action.Enter(machine);
        }
    }

    public override void Enter<T>(BaseStateMachine machine, T param)
    {
        foreach (var action in Action)
        {
            action.Enter(machine, param);
        }
    }

    public override void Exit(BaseStateMachine machine)
    {
        foreach (var action in Action)
        {
            action.Exit(machine);
        }
    }
}
