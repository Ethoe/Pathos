using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMAction : ScriptableObject
{
    public abstract void Execute(BaseStateMachine stateMachine);
    public virtual void Enter<T>(BaseStateMachine stateMachine, T param) { }
    public virtual void Enter(BaseStateMachine stateMachine) { }
    public virtual void Exit(BaseStateMachine stateMachine) { }
}
