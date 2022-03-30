using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class DashAbility : ScriptableAbility
{
    public float distance;
    public float speed;
    public override TimedAbility InitializeAbility(GameObject source)
    {
        return new TimedDashAbility(this, source);
    }
}
