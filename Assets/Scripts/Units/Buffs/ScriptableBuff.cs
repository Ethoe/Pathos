using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    ConsumeableBuff,
    PermanentBuff,
    TimedBuff,
}
public abstract class ScriptableBuff : ScriptableObject
{
    /// Time duration of the buff in seconds.
    public float Duration;

    /// Duration is increased each time the buff is applied.
    public bool IsDurationStacked;

    // Effect value is increased each time the buff is applied.
    public bool IsEffectStacked;

    // Duration does not tick down.
    public BuffType BuffType;

    public abstract TimedBuff InitializeBuff(GameObject obj);
}
