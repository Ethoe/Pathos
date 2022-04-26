using UnityEngine;

public abstract class EnemyBaseState : ScriptableObject
{
    public abstract void Execute(BaseStateMachine machine);
    public abstract void Enter(BaseStateMachine machine);
    public abstract void Enter<T>(BaseStateMachine machine, T param);
    public abstract void Exit(BaseStateMachine machine);
}
