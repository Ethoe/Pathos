using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBolt : SkillshotController
{
    public override float FlatDamage()
    {
        return 10.0f + (stats.Attack.Value * .80f); // 80% scaling
    }
}
