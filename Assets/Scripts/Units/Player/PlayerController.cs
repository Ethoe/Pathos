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
    public SecondAbilityState secondAbility;
    public bool shoot;
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

    // Private Vars
    private Rigidbody2D rigidbody2d;
    protected Vector2 oldPosition;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(Events.DealDamageTrigger, ReceiveDamage);
        EventManager.StartListening(Events.DealDamageTrigger, DealDamage);
        EventManager.StartListening(Events.DoorwayTriggerEnter, ExitRoom);

        buffBar = gameObject.GetComponent<BuffableEntity>();

        abilities = GetComponent<AbilityHolder>();

        animator = GetComponent<Animator>();
        oldPosition = this.gameObject.transform.position;
        shoot = false;


        playerInput = GetComponent<PlayerInput>();
        controlSM = new PlayerStateMachine();
        idle = new IdleState(this, controlSM);
        moving = new MovingState(this, controlSM);
        attacking = new AttackingState(this, controlSM);
        attackMoving = new AttackMovingState(this, controlSM);
        secondAbility = new SecondAbilityState(this, controlSM);
        controlSM.Initialize(idle);

        GameManager.Instance.player = gameObject;

        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        statsComponent = GetComponent<StatBlockComponent>();
        Debug.Log(statsComponent.stats.MoveSpeed.Value);
    }

    // Update is called once per frame
    void Update()
    {
        controlSM.CurrentState.HandleInput();
        controlSM.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        Vector2 moving = ((Vector2)this.gameObject.transform.position - oldPosition).normalized;
        animator.SetFloat("Move X", moving.x);
        animator.SetFloat("Move Y", moving.y);

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

    public void PlayerMove(Vector2 target)
    {
        oldPosition = transform.position;
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * statsComponent.stats.MoveSpeed.Value));
    }

    public void PlayerMove(Vector2 target, float moveSpeed)
    {
        oldPosition = transform.position;
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed));
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
}
