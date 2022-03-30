using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSkillshotAbility : TimedAbility
{
    public TimedSkillshotAbility(ScriptableAbility ability, GameObject source) : base(ability, source)
    {
    }

    public override void Activate(GameObject target, Vector2 direction)
    {
        base.Activate(target, direction);

        direction = direction - (Vector2)Source.transform.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90.0f;
        GameObject projectileObject = GameObject.Instantiate(Ability.ability, Source.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        SkillshotController projectile = projectileObject.GetComponent<SkillshotController>();
        projectile.owner = Source;
        projectile.range = ((SkillshotAbility)Ability).range;
        projectile.stopOnHit = ((SkillshotAbility)Ability).stopOnHit;
        projectile.speed = ((SkillshotAbility)Ability).speed;
        projectile.Launch(direction);
    }

    public override void End() { }
}
