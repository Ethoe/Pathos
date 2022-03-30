using UnityEngine;

public abstract class TimedAbility
{
    public ScriptableAbility Ability { get; }
    public AbilityState state;
    public float Cooldown;
    public float CastTime;

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
                break;
            case AbilityState.casting:
                if (CastTime > 0)
                {
                    CastTime -= delta;
                }
                else
                {
                    state = AbilityState.cooldown;
                    Cooldown = Ability.Cooldown;
                }
                break;
            case AbilityState.cooldown:
                if (Cooldown > 0)
                {
                    Cooldown -= delta;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    public virtual void Activate(GameObject target, Vector2 direction)
    {
        state = AbilityState.casting;
        CastTime = Ability.CastTime;
    }

    public abstract void End();
}