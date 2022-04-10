using UnityEngine;

public class TimedAttackBuff : TimedBuff
{
    private readonly StatBlockComponent statBlock;

    public TimedAttackBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        statBlock = obj.GetComponent<StatBlockComponent>();
    }

    protected override void ApplyEffect()
    {
        AttackBuff AttackBuff = (AttackBuff)Buff;

        if (!Mathf.Approximately(AttackBuff.FlatIncrease, 0))
        {
            statBlock.stats.Attack.AddModifier(new StatModifier(AttackBuff.FlatIncrease, StatModType.Flat, this));
        }

        if (!Mathf.Approximately(AttackBuff.PercentIncrease, 0))
        {
            statBlock.stats.Attack.AddModifier(new StatModifier(AttackBuff.PercentIncrease, StatModType.PercentAdd, this));
        }
    }

    protected override void TickEffect() { }

    public override void End()
    {
        statBlock.stats.Attack.RemoveAllModifiersFromSource(this);
    }
}
