using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Stat/SpeedBuff")]
public class SpeedBuff : ScriptableBuff
{
    public float SpeedIncrease;

    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedSpeedBuff(this, obj);
    }
}
