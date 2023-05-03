using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Stats/HealBuff")]
public class HealBuff : ScriptableBuff
{
    public float FlatHeal;
    [Range(0, 1)]
    public float PercentHeal;
    public override TimedBuff InitializeBuff(GameObject obj)
    {
        switch (this.BuffType)
        {
            case BuffType.ConsumeableBuff:
                {
                    return new ConsHealBuff(this, obj);
                }
            case BuffType.PermanentBuff:
                {
                    return new ConsHealBuff(this, obj);
                }
            case BuffType.TimedBuff:
                {
                    return new ConsHealBuff(this, obj);
                }
            default: return null;
        }
    }
}
