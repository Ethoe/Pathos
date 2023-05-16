using UnityEngine;

public abstract class ObjectOnHitModifier
{
    public ScriptableOnHitModifier Modifier { get; }

    public abstract void Activate();
    public abstract StatModifier GetStatModifier(DamageContext context);
    public abstract void End();
}
