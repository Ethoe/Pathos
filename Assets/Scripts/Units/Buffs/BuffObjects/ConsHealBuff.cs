using UnityEngine;
using System.Collections.Generic;

// Consumable
public class ConsHealBuff : TimedBuff
{
    private HealBuff healBuff;
    private readonly StatBlockComponent statBlock;

    public ConsHealBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        statBlock = obj.GetComponent<StatBlockComponent>();
        healBuff = (HealBuff)buff;
    }

    protected override void ApplyEffect()
    {
        if (!Mathf.Approximately(healBuff.FlatHeal, 0))
        {
            statBlock.stats.Health.CurrentValue += healBuff.FlatHeal;
        }

        if (!Mathf.Approximately(healBuff.PercentHeal, 0))
        {
            statBlock.stats.Health.CurrentValue += statBlock.stats.Health.BaseValue * healBuff.PercentHeal;
        }
    }

    protected override void TickEffect() { }

    public override void End() { }
}
