using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/CC/StunBuff")]
public class StunBuff : ScriptableBuff
{
    public EnemyState StunState;
    public override TimedBuff InitializeBuff(GameObject obj)
    {
        return new TimedStunBuff(this, obj);
    }
}
