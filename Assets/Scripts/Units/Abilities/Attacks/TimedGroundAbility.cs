using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedGroundAbility : TimedAbility
{
    public TimedGroundAbility(ScriptableAbility ability, GameObject source) : base(ability, source)
    {
    }

    public override void Activate(GameObject target, Vector2 origin)
    {
        base.Activate(target, origin);
        GameObject groundAbility = GameObject.Instantiate(Ability.ability, origin, Quaternion.identity);
        GroundAbilityController groundTarget = groundAbility.GetComponent<GroundAbilityController>();
        groundTarget.owner = Source;
        groundTarget.duration = ((GroundAbility)Ability).duration;
    }

    public override void End() { }
}
