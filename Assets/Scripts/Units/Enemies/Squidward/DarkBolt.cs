using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBolt : SkillshotController
{

    public float fadeDistance = 1f;
    private Vector2 fadeStartLocation;

    public override float FlatDamage()
    {
        return 10.0f + (stats.Attack.Value * .80f); // 80% scaling
    }

    protected override void EndSkillshot()
    {
        Color color_i = new Color(1, 1, 1, 1);
        Color color_f = new Color(1, 1, 1, 0);
        if (fadeStartLocation == Vector2.zero)
            fadeStartLocation = transform.position;
        float progress = (Vector2.Distance((Vector2)transform.position, fadeStartLocation)) / fadeDistance;
        if (progress <= 1)
        {
            //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
            sprite.color = Color.Lerp(color_i, color_f, progress);
        }
        else Destroy(gameObject);
    }
}
