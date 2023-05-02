using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Effects/ThornmailBuff")]
public class ThornmailBuff : ScriptableBuff
{
    public float FlatDamage;
    [Range(0, 1)]
    public float PercentDamage;

    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedThornmailBuff(this, obj);
    }
}
