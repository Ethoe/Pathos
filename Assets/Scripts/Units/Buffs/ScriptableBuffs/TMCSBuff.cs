using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/TMCSBuff")]
public class TMCSBuff : ScriptableBuff
{
    public float SpeedIncrease;
    public float AttackSpeedIncrease;

    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedTMCSBuff(this, obj);
    }
}
