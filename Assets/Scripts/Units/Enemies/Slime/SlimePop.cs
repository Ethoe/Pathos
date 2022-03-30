using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePop : GroundAbilityController
{
    public float fadeDuration = 1.0f;
    private float fadeStartTime;

    public override float FlatDamage()
    {
        return 10.0f + (stats.Attack.Value * .80f); // 80% scaling
    }

    protected override void EndSkillshot()
    {
        Color color_i = new Color(1, 1, 1, 1);
        Color color_f = new Color(1, 1, 1, 0);
        if (fadeStartTime == 0)
            fadeStartTime = Time.time;
        float progress = (Time.time - fadeStartTime) / fadeDuration;
        if (progress <= 1)
        {
            //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
            sprite.color = Color.Lerp(color_i, color_f, progress);
        }
        else Destroy(gameObject);
    }
}
