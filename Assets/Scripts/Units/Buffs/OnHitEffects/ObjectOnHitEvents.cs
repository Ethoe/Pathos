using UnityEngine;

public abstract class ObjectOnHitEvents
{
    public ScriptableOnHitEvents EventSO { get; }

    public abstract void Activate();
    public abstract void EventTrigger(DamageContext context);
    public abstract void End();
}
