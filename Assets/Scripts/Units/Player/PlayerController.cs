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
    public GameObject autoAttackProjectile;
    public GameObject skillShotProjectile;
    public PlayerControls controls;
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

        controls = new PlayerControls();

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

    public void AutoAttack(GameObject target)
    {
        if (statsComponent.stats.isMelee)
        {
            GameManager.Instance.CalculateDamage(
                new DamageContext(
                    this.gameObject,
                    target,
                    statsComponent.stats.Attack.Value,
                    Tools.percentChance(statsComponent.stats.CritChance.Value),
                    DamageDealtType.Basic
                    )
                );
        }
        else
        {
            GameObject autoAttack = Instantiate(autoAttackProjectile, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            ProjectileController projectile = autoAttack.GetComponent<ProjectileController>();
            projectile.target = target;
            projectile.source = this.gameObject;
            projectile.isCrit = Tools.percentChance(statsComponent.stats.CritChance.Value);
        }
    }

    public void AutoAttackFire()
    {
        shoot = true;
    }

    public void Skillshot(Vector2 direction)
    {
        GameObject projectileObject = Instantiate(skillShotProjectile, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        SkillshotController projectile = projectileObject.GetComponent<SkillshotController>();
        projectile.Launch(direction, 300);
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
