using System.Collections.Generic;
using UnityEngine;

public class StatBlockComponent : MonoBehaviour
{
    public bool Player = false;
    public StatBlock originalStats;
    public StatBlock stats;

    void Awake()
    {
        stats = Instantiate<StatBlock>(originalStats);
    }

    void Start()
    {
        EventManager.StartListening(Events.DealDamageTrigger, ReceiveDamage);
        EventManager.StartListening(Events.DealDamageTrigger, DealDamage);

        if (!Player)
        {
            GameManager.Instance.AddEnemy(gameObject);
        }
        else
        {
            GameManager.Instance.player = gameObject;
        }
    }

    void Update()
    {
        if (stats.Health.CurrentValue <= 0)
        {
            EventManager.TriggerEvent(Events.UnitDiedTrigger, new Dictionary<string, object> { { "source", gameObject } });
            if (!Player)
                GameManager.Instance.RemoveEnemy(gameObject);
            gameObject.SetActive(false);
        }
    }

    protected virtual void ReceiveDamage(Dictionary<string, object> message)
    {
        var damage = (DamageContext)message["damage"];
        if (damage.target == gameObject)
        {
            stats.Health.CurrentValue -= damage.baseDamage;
        }
    }

    protected virtual void DealDamage(Dictionary<string, object> message)
    {
        var damage = (DamageContext)message["damage"];
        if (damage.source == gameObject)
        {
            stats.Health.CurrentValue += damage.baseDamage * stats.LifeSteal.Value;
        }
    }

    protected void OnDestroy()
    {
        EventManager.StopListening(Events.DealDamageTrigger, ReceiveDamage);
        EventManager.StopListening(Events.DealDamageTrigger, DealDamage);
    }
}
