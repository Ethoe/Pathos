using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedAttackAbility : TimedAbility
{
    private StatBlockComponent statsComponent;
    public TimedAttackAbility(ScriptableAbility ability, GameObject source) : base(ability, source)
    {
        statsComponent = source.GetComponent<StatBlockComponent>();
    }

    public override void Activate(GameObject target, Vector2 direction, int layer)
    {
        base.Activate(target, direction, layer);
        GameObject autoAttack = GameObject.Instantiate(Ability.ability, (Vector2)Source.transform.position + Vector2.up * 0.5f, Quaternion.identity);
        autoAttack.layer = layer;
        ProjectileController projectile = autoAttack.GetComponent<ProjectileController>();
        projectile.target = target;
        projectile.source = Source;
        projectile.isCrit = Tools.percentChance(statsComponent.stats.CritChance.Value);
    }

    public override void End() { }
}
