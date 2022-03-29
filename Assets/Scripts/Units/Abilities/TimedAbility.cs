using UnityEngine;

public abstract class TimedAbility
{
    public ScriptableAbility Ability { get; }
    public AbilityState state;
    public float Cooldown;

    protected readonly GameObject Source;

    public TimedAbility(ScriptableAbility ability, GameObject source)
    {
        Ability = ability;
        Source = source;
        state = AbilityState.ready;
        Cooldown = Ability.Cooldown;
    }
    public void Tick(float delta)
    {
        switch (state)
        {
            case AbilityState.ready:
            case AbilityState.casting:
            case AbilityState.cooldown:
                Cooldown -= delta;
                break;
        }
    }

    public abstract void Activate(GameObject target, Vector2 direction);

    public abstract void End();
}