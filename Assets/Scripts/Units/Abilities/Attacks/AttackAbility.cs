using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack")]
public class AttackAbility : ScriptableAbility
{
    public override TimedAbility InitializeAbility(GameObject source)
    {
        return new TimedAttackAbility(this, source);
    }
}
