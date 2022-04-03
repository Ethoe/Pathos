using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    // Public Vars
    public PlayerStateMachine controlSM;
    public IdleState idle;
    public MovingState moving;
    public AttackingState attacking;
    public AttackMovingState attackMoving;
    public PlayerAbilityState ability;
    public bool shoot;
    public int layer = 10; //Physics collider level ally projectile
    public LayerMask hit;

    [HideInInspector]
    public StatBlockComponent statsComponent;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public BuffableEntity buffBar;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public AbilityHolder abilities;
    public MovementController movement;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(Events.DealDamageTrigger, ReceiveDamage);
        EventManager.StartListening(Events.DealDamageTrigger, DealDamage);
        EventManager.StartListening(Events.DoorwayTriggerEnter, ExitRoom);
        EventManager.StartListening(Events.AbilityAnimationTrigger, TriggerAnimationListener);

        buffBar = gameObject.GetComponent<BuffableEntity>();

        abilities = GetComponent<AbilityHolder>();
        movement = GetComponent<MovementController>();

        animator = GetComponent<Animator>();
        shoot = false;


        playerInput = GetComponent<PlayerInput>();
        controlSM = new PlayerStateMachine();
        idle = new IdleState(this, controlSM);
        moving = new MovingState(this, controlSM);
        attacking = new AttackingState(this, controlSM);
        attackMoving = new AttackMovingState(this, controlSM);
        ability = new PlayerAbilityState(this, controlSM);
        controlSM.Initialize(idle);

        GameManager.Instance.player = gameObject;

        spriteRenderer = GetComponent<SpriteRenderer>();

        statsComponent = GetComponent<StatBlockComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moving = movement.Direction;
        animator.SetFloat("Move X", moving.x);
        animator.SetFloat("Move Y", moving.y);

        controlSM.CurrentState.HandleInput();
        controlSM.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        controlSM.CurrentState.PhysicsUpdate();
    }

    public void AutoAttackFire()
    {
        shoot = true;
    }

    public void TriggerAnimation(int param)
    {
        animator.Play(param, 0, 0.0f);
    }

    private void ExitRoom(Dictionary<string, object> message)
    {
        controlSM.ChangeState(idle);
    }

    public void OnDestroy()
    {
        EventManager.StopListening(Events.DealDamageTrigger, ReceiveDamage);
        EventManager.StopListening(Events.DealDamageTrigger, DealDamage);
        EventManager.StopListening(Events.DoorwayTriggerEnter, ExitRoom);
        EventManager.StopListening(Events.AbilityAnimationTrigger, TriggerAnimationListener);
    }

    protected virtual void ReceiveDamage(Dictionary<string, object> message)
    {
        var damage = (DamageContext)message["damage"];
        if (damage.target == gameObject)
        {
            statsComponent.stats.Health.CurrentValue -= damage.baseDamage;
        }
    }

    protected virtual void DealDamage(Dictionary<string, object> message)
    {
        var damage = (DamageContext)message["damage"];
        if (damage.source == gameObject)
        {
            statsComponent.stats.Health.CurrentValue += damage.baseDamage * statsComponent.stats.LifeSteal.Value;
        }
    }

    public void TriggerAnimationListener(Dictionary<string, object> message)
    {
        var source = (GameObject)message["source"];
        var param = (int)message["param"];
        if (source == gameObject)
        {
            animator.Play(param, 0, 0.0f);
        }
    }
}
