using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDashAbility : TimedAbility
{
    private MovementController movement;
    private Rigidbody2D rigidbody2D;
    public TimedDashAbility(ScriptableAbility ability, GameObject source) : base(ability, source)
    {
        state = AbilityState.ready;
        movement = source.GetComponent<MovementController>();
        rigidbody2D = source.GetComponent<Rigidbody2D>();
    }

    public override void Activate(GameObject target, Vector2 direction, int layer)
    {
        base.Activate(target, direction, layer);
        EventManager.TriggerEvent(Events.AbilityAnimationTrigger, new Dictionary<string, object> { { "source", Source }, { "param", Animator.StringToHash("DashBlend") } });

        Vector2 startPos = Source.transform.position;
        movement.Target = ((direction - startPos).normalized * ((DashAbility)Ability).distance) + startPos;
        movement.MoveSpeed = ((DashAbility)Ability).speed;
    }

    public override void TickEffect()
    {
        base.TickEffect();
        if (!movement.IsMoving && state == AbilityState.casting)
        {
            state = AbilityState.cooldown;
            Cooldown = Ability.Cooldown;
        }
    }

    public override void End()
    {
        
    }
}
