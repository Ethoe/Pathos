using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Public Vars
    public GameObject target { set { targetting = true; currentTarget = value; } }
    public GameObject source;
    public bool isCrit;

    // Private Vars
    private GameObject currentTarget;
    private bool targetting;

    void Update()
    {
        if (!currentTarget)
        {
            Destroy(gameObject);
        }
        else if (targetting)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distance <= 0.1f)
            {
                GameManager.Instance.CalculateDamage(new DamageContext(source, currentTarget, 0, isCrit, DamageDealtType.Basic));
                Destroy(gameObject);
            }
            else
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 100;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, Time.deltaTime * 7.5f);
            }
        }
    }
}
