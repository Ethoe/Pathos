using UnityEngine;

public abstract class EnemyBaseState : ScriptableObject
{
    public abstract void Execute(BaseStateMachine machine);
}
