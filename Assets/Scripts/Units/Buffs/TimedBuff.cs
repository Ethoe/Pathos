using UnityEngine;

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
        if (!Buff.IsPermanent)
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

        if (!Buff.IsPermanent)
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