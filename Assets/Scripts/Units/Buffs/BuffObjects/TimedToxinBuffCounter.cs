using UnityEngine;
using System.Collections.Generic;
public class TimedToxinBuffCounter : TimedBuff
{
    private ToxinBuffCounter toxinBuffCounter;
    private int stacks;

    public TimedToxinBuffCounter(ScriptableBuff buff, GameObject obj) : base(buff, obj)
    {
        toxinBuffCounter = (ToxinBuffCounter)buff;
        stacks = 0;
    }

    protected override void ApplyEffect()
    {
        stacks += 1;
    }

    protected override void TickEffect() { }

    public override void End()
    {
        EventManager.TriggerEvent(Events.ToxinActivateTrigger, new Dictionary<string, object> {
            {
                "toxinContext",
                new ToxinContext(
                    source: toxinBuffCounter.source,
                    target: Obj,
                    stacks: stacks
                )
            }
        });
        stacks = 0;
    }

}
