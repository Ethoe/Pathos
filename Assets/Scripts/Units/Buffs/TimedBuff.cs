using UnityEngine;

public class BuffContext
{
    public TimedBuff buff;
    public GameObject source;
    public GameObject target;

    public BuffContext(GameObject source, GameObject target, TimedBuff buff)
    {
        this.source = source;
        this.target = target;
        this.buff = buff;
    }
}

public abstract class TimedBuff
{
    protected float Duration;
    protected int EffectStacks;
    public ScriptableBuff Buff { get; }
    protected readonly GameObject Obj;
    public bool IsFinished;

    public TimedBuff(ScriptableBuff buff, GameObject obj)
    {
        Buff = buff;
        Obj = obj;
    }
    public void Tick(float delta)
    {
        if (Buff.BuffType != BuffType.PermanentBuff)
        {
            Duration -= delta;
            if (Duration <= 0)
            {
                End();
                IsFinished = true;
            }
        }
        TickEffect();
    }

    public void Activate()
    {
        if (Buff.IsEffectStacked || Duration <= 0)
        {
            ApplyEffect();
            EffectStacks++;
        }

        if (Buff.BuffType != BuffType.PermanentBuff)
        {
            if (Buff.IsDurationStacked || Duration <= 0)
            {
                Duration += Buff.Duration;
            }
            else
            {
                Duration = Buff.Duration;
            }
        }
    }

    protected abstract void TickEffect();
    protected abstract void ApplyEffect();
    public abstract void End();
}