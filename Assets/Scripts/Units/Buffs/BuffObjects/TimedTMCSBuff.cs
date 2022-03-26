using UnityEngine;

public class TimedTMCSBuff : TimedBuff
{
    private readonly StatBlockComponent statBlock;
    private StatModifier speedModifier;
    private StatModifier attackSpeedModifier;

    public TimedTMCSBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        statBlock = obj.GetComponent<StatBlockComponent>();
        speedModifier = new StatModifier(((TMCSBuff)buff).SpeedIncrease, StatModType.Flat, this);
        attackSpeedModifier = new StatModifier(((TMCSBuff)buff).AttackSpeedIncrease, StatModType.Flat, this);
    }

    protected override void ApplyEffect()
    {
        statBlock.stats.MoveSpeed.AddModifier(speedModifier);
        statBlock.stats.AttackSpeed.AddModifier(attackSpeedModifier);
    }

    protected override void TickEffect() { }

    public override void End()
    {
        statBlock.stats.MoveSpeed.RemoveAllModifiersFromSource(this);
        statBlock.stats.AttackSpeed.RemoveAllModifiersFromSource(this);
    }
}
