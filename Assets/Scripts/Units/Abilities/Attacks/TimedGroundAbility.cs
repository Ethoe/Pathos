using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedGroundAbility : TimedAbility
{
    public TimedGroundAbility(ScriptableAbility ability, GameObject source) : base(ability, source)
    {
    }

    public override void Activate(GameObject target, Vector2 origin, int layer)
    {
        base.Activate(target, origin, layer);
        GameObject groundAbility = GameObject.Instantiate(Ability.ability, origin, Quaternion.identity);
        groundAbility.layer = layer;
        GroundAbilityController groundTarget = groundAbility.GetComponent<GroundAbilityController>();
        groundTarget.owner = Source;
        groundTarget.duration = ((GroundAbility)Ability).duration;
    }

    public override void End() { }
}
