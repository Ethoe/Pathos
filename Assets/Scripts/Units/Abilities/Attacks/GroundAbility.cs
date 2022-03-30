using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/GroundAbility")]
public class GroundAbility : ScriptableAbility
{
    public float duration;
    public override TimedAbility InitializeAbility(GameObject source)
    {
        return new TimedGroundAbility(this, source);
    }
}
