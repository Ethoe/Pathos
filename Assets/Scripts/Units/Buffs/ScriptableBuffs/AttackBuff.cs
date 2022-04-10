using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/AttackBuff")]
public class AttackBuff : ScriptableBuff
{
    public float FlatIncrease;
    [Range(0, 1)]
    public float PercentIncrease;

    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedAttackBuff(this, obj);
    }
}
