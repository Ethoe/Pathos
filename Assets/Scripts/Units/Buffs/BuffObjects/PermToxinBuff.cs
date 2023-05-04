using UnityEngine;
using System.Collections.Generic;

public class ToxinContext
{
    public int stacks;
    public GameObject source;
    public GameObject target;

    public ToxinContext(GameObject source, GameObject target, int stacks)
    {
        this.source = source;
        this.target = target;
        this.stacks = stacks;
    }
}

public class PermToxinBuff : TimedBuff
{
    private ToxinBuff toxinBuff;
    private readonly StatBlockComponent statBlock;

    public PermToxinBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {

        statBlock = obj.GetComponent<StatBlockComponent>();
        toxinBuff = (ToxinBuff)buff;
    }

    protected override void ApplyEffect()
    {
        EventManager.StartListening(Events.DealDamageTrigger, DealDamage);
        EventManager.StartListening(Events.ToxinActivateTrigger, ActivateToxin);
    }

    protected override void TickEffect() { }

    public override void End()
    {
        EventManager.StopListening(Events.DealDamageTrigger, DealDamage);
        EventManager.StopListening(Events.ToxinActivateTrigger, ActivateToxin);
    }

    public void ActivateToxin(Dictionary<string, object> message)
    {
        var toxin = (ToxinContext)message["toxinContext"];
        if (toxin.source == Obj)
        {
            GameManager.Instance.CalculateDamage(new DamageContext(toxin.source, toxin.target, toxin.stacks * (statBlock.stats.Attack.Value * 1f), false, DamageDealtType.Item));
        }
    }

    public void DealDamage(Dictionary<string, object> message)
    {
        var damage = (DamageContext)message["damage"];
        if (damage.source == Obj && damage.type == DamageDealtType.Basic)
        {
            EventManager.TriggerEvent(Events.AddBuffTrigger, new Dictionary<string, object> {
                {
                    "buff",
                    new BuffContext(
                        source: Obj,
                        target: damage.target,
                        buff: toxinBuff.toxinBuffCounter.InitializeBuff(damage.target, Obj)
                    )
                }
            });
        }
    }
}
