using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBolt : SkillshotController
{
    public override float FlatDamage()
    {
        return 100.0f + (stats.Magic.Value * .80f); // 80% scaling
    }
}
