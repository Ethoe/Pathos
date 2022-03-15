using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public float damage;
    public bool isCrit;
    public DamageDealtType damageType;

    public DamageInfo(float damage, DamageDealtType damageType, bool isCrit)
    {
        this.damage = damage;
        this.damageType = damageType;
        this.isCrit = isCrit;
    }
}
