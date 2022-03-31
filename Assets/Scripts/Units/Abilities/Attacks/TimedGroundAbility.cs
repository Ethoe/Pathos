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
        Vector2 startPos = Source.transform.position;
        Vector2 moveTarget = ((origin - startPos).normalized * ((GroundAbility)Ability).castRange) + startPos;
        GameObject groundAbility = GameObject.Instantiate(Ability.ability, moveTarget, Quaternion.identity);
        groundAbility.layer = layer;
        GroundAbilityController groundTarget = groundAbility.GetComponent<GroundAbilityController>();
        groundTarget.owner = Source;
        groundTarget.duration = ((GroundAbility)Ability).duration;
    }

    public override void End() { }
}
