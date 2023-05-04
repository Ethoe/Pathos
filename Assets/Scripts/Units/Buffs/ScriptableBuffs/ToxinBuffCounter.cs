using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Items/Toxin/ToxinBuffCounter")]
public class ToxinBuffCounter : ScriptableBuff
{
    public GameObject source;
    public override TimedBuff InitializeBuff(GameObject obj)
    {
        switch (this.BuffType)
        {
            case BuffType.ConsumeableBuff:
                {
                    return new TimedToxinBuffCounter(this, obj);
                }
            case BuffType.PermanentBuff:
                {
                    return new TimedToxinBuffCounter(this, obj);
                }
            case BuffType.TimedBuff:
                {
                    return new TimedToxinBuffCounter(this, obj);
                }
            default: return null;
        }
    }

    public TimedBuff InitializeBuff(GameObject obj, GameObject source)
    {
        this.source = source;
        switch (this.BuffType)
        {
            case BuffType.ConsumeableBuff:
                {
                    return new TimedToxinBuffCounter(this, obj);
                }
            case BuffType.PermanentBuff:
                {
                    return new TimedToxinBuffCounter(this, obj);
                }
            case BuffType.TimedBuff:
                {
                    return new TimedToxinBuffCounter(this, obj);
                }
            default: return null;
        }
    }
}
