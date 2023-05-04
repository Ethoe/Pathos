using UnityEngine;

public enum DamageDealtType
{
    Basic,
    Ability,
    Item,
    True,
}

public class DamageContext
{
    public GameObject source;
    public GameObject target;
    public float baseDamage;
    public bool isCrit;
    public DamageDealtType type;

    public DamageContext(GameObject source, GameObject target, float baseDamage, bool isCrit, DamageDealtType type)
    {
        this.source = source;
        this.target = target;
        this.baseDamage = baseDamage;
        this.isCrit = isCrit;
        this.type = type;
    }
}
