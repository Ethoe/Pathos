using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Items/Toxin/ToxinBuff")]
public class ToxinBuff : ScriptableBuff
{
    public ToxinBuffCounter toxinBuffCounter;

    public override TimedBuff InitializeBuff(GameObject obj)
    {
        switch (this.BuffType)
        {
            case BuffType.ConsumeableBuff:
                {
                    return new PermToxinBuff(this, obj);
                }
            case BuffType.PermanentBuff:
                {
                    return new PermToxinBuff(this, obj);
                }
            case BuffType.TimedBuff:
                {
                    return new PermToxinBuff(this, obj);
                }
            default: return null;
        }
    }
}
