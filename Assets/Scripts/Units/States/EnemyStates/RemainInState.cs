using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Remain In State", fileName = "RemainInState")]
public class RemainInState : EnemyBaseState
{
    public override void Execute(BaseStateMachine machine) { }
    public override void Enter(BaseStateMachine machine) { }
    public override void Enter<T>(BaseStateMachine machine, T param) { }
    public override void Exit(BaseStateMachine machine) { }
}
