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
    // Start is called before the first frame update
    void Awake()
    {
    }

    void Update()
    {
        if (!currentTarget)
        {
            Destroy(gameObject);
        }
        if (targetting)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distance <= 0.1f)
            {
                Debug.Log(damage());
                EffectManager.Instance.DamageTextAnimation(((int)damage()).ToString(), DamageDealtType.Physical, isCrit, 7.0f, currentTarget);
                Destroy(gameObject);
            }
            else
            {
                Vector3 direction = currentTarget.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 100;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, Time.deltaTime * 5);
            }
        }
    }

    private float damage()
    {
        StatBlock sourceStats = source.GetComponent<StatBlockComponent>().stats;
        StatBlock targetStats = currentTarget.GetComponent<StatBlockComponent>().stats;
        CharacterStat calculator = new CharacterStat(sourceStats.Attack.Value);
        if (isCrit)
        {
            calculator.AddModifier(new StatModifier(sourceStats.CritDamage.Value, StatModType.PercentMult));
        }
        calculator.AddModifier(new StatModifier(Tools.ResistDamageMultiplier(targetStats.Armor.Value), StatModType.PercentMult));
        return calculator.Value;
    }
}
