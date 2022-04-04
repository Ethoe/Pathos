using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Skillshot")]
public class SkillshotAbility : ScriptableAbility
{
    public bool stopOnHit;
    public float speed;
    public override TimedAbility InitializeAbility(GameObject source)
    {
        return new TimedSkillshotAbility(this, source);
    }
}
