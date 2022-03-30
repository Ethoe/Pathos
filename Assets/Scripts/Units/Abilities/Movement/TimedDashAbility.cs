using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDashAbility : TimedAbility
{
    private Vector2 startPos;
    private Vector2 moveTarget;
    private Rigidbody2D rigidbody2D;
    public TimedDashAbility(ScriptableAbility ability, GameObject source) : base(ability, source)
    {
        state = AbilityState.ready;
        rigidbody2D = source.GetComponent<Rigidbody2D>();
    }

    public override void Activate(GameObject target, Vector2 direction, int layer)
    {
        base.Activate(target, direction, layer);
        EventManager.TriggerEvent(Events.AbilityAnimationTrigger, new Dictionary<string, object> { { "source", Source }, { "param", Animator.StringToHash("DashBlend") } });

        startPos = Source.transform.position;
        moveTarget = ((direction - startPos).normalized * ((DashAbility)Ability).distance) + startPos; // Get angle of mouse and multiply vector by dashdistance magnitude
    }

    public override void TickEffect()
    {
        base.TickEffect();
        float distance = Vector2.Distance(Source.transform.position, startPos);
        if (distance >= ((DashAbility)Ability).distance && state == AbilityState.casting)
        {
            state = AbilityState.cooldown;
            Cooldown = Ability.Cooldown;
        }
    }

    public override void LogicTick(float delta)
    {
        base.LogicTick(delta);
        if (state == AbilityState.casting)
            move(moveTarget, ((DashAbility)Ability).speed, delta);
    }
    public override void End() { }
    public void move(Vector2 target, float moveSpeed, float delta)
    {
        rigidbody2D.MovePosition(Vector2.MoveTowards(Source.transform.position, target, delta * moveSpeed));
    }
}
