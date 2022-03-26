using UnityEngine;

public class TimedSpeedBuff : TimedBuff
{
    private readonly StatBlockComponent statBlock;

    public TimedSpeedBuff(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        statBlock = obj.GetComponent<StatBlockComponent>();
    }

    protected override void ApplyEffect()
    {
        SpeedBuff speedBuff = (SpeedBuff)Buff;
        statBlock.stats.MoveSpeed.AddModifier(new StatModifier(speedBuff.SpeedIncrease, StatModType.Flat, this));
    }

    protected override void TickEffect() { }

    public override void End()
    {
        statBlock.stats.MoveSpeed.RemoveAllModifiersFromSource(this);
    }
}
