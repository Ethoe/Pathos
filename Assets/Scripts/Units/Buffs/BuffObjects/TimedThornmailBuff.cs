using UnityEngine;
using System.Collections.Generic;

public class TimedThornmailBuff : TimedBuff
{
    private ThornmailBuff thornmailBuff;
    public TimedThornmailBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        thornmailBuff = (ThornmailBuff)Buff;
    }

    protected override void ApplyEffect()
    {
        EventManager.StartListening(Events.DealDamageTrigger, ReceiveDamage);
    }

    protected override void TickEffect() { }

    public override void End()
    {
        EventManager.StopListening(Events.DealDamageTrigger, ReceiveDamage);
    }

    public void ReceiveDamage(Dictionary<string, object> message)
    {
        var damage = (DamageContext)message["damage"];
        if (damage.target == Obj && (damage.type == DamageDealtType.Basic || damage.type == DamageDealtType.Ability))
        {
            GameManager.Instance.CalculateDamage(new DamageContext(damage.target, damage.source, thornmailBuff.FlatDamage + (thornmailBuff.PercentDamage * damage.baseDamage), false, DamageDealtType.Item));
        }
    }
}
