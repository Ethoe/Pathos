using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/StunBuff")]
public class StunBuff : ScriptableBuff
{
    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedStunBuff(this, obj);
    }
}
