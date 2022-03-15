using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazyHealthBar : MonoBehaviour
{
    private float currHPSlow;

    private StatBlockComponent statsComp;
    private BoxCollider2D boundingBox;
    public GameObject target;
    public Image barFast, barSlow;
    // Start is called before the first frame update

    void Start()
    {
        boundingBox = target.GetComponent<BoxCollider2D>();
        statsComp = target.GetComponent<StatBlockComponent>();
        currHPSlow = statsComp.stats.Health.CurrentValue;
    }

    // Update is called once per frame
    float t = 0;
    void Update()
    {
        if (!target)
        {
            Destroy(gameObject);
        }
        else
        {
            Bounds bounds = boundingBox.bounds;
            transform.position = new Vector2(bounds.center.x, bounds.center.y - bounds.extents.y) + new Vector2(0, -.25f);
        }
        //interpolating slowHP and currentHP inf unequal
        if (!Mathf.Approximately(currHPSlow, statsComp.stats.Health.CurrentValue))
        {
            currHPSlow = Mathf.Lerp(currHPSlow, statsComp.stats.Health.CurrentValue, t);
            t += .05f * Time.deltaTime;
        }
        else
        {
            t = 0;
            //resetting interpolator
        }

        //Setting fill amount
        barFast.fillAmount = statsComp.stats.Health.CurrentValue / statsComp.stats.Health.Value;
        barSlow.fillAmount = currHPSlow / statsComp.stats.Health.Value;
    }
}
