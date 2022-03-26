using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    // Public Vars
    public StateMachine controlSM;
    public IdleState idle;
    public MovingState moving;
    public AttackingState attacking;
    public AttackMovingState attackMoving;
    public SecondAbilityState secondAbility;
    public GameObject autoAttackProjectile;
    public GameObject skillShotProjectile;
    public StatBlock stats;
    public PlayerControls controls;
    public LayerMask hit;
    public bool shoot;
    public SpriteRenderer spriteRenderer;
    public BuffableEntity buffBar;

    // Private Vars
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    protected Vector2 oldPosition;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.onDoorwayTriggerEnter += ExitRoom;
        EventManager.instance.onDealDamage += ReceiveDamage;
        EventManager.instance.onDealDamage += DealDamage;

        buffBar = gameObject.GetComponent<BuffableEntity>();

        animator = GetComponent<Animator>();
        oldPosition = this.gameObject.transform.position;
        shoot = false;

        controlSM = new StateMachine();
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

        stats = GetComponent<StatBlockComponent>().stats;
        JsonUtility.FromJsonOverwrite(Tools.LoadResourceTextfile("Stats/Players/pika"), stats);
    }

    // Update is called once per frame
    void Update()
    {
        controlSM.CurrentState.HandleInput();
        controlSM.CurrentState.LogicUpdate();

        animator.SetFloat("Attack Speed", stats.AttackSpeed.Value);
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
        if (stats.isMelee)
        {
            GameManager.Instance.CalculateDamage(new DamageContext(this.gameObject, target, stats.Attack.Value, Tools.percentChance(stats.CritChance.Value), DamageDealtType.Basic));
        }
        else
        {
            GameObject autoAttack = Instantiate(autoAttackProjectile, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            ProjectileController projectile = autoAttack.GetComponent<ProjectileController>();
            projectile.target = target;
            projectile.source = this.gameObject;
            projectile.isCrit = Tools.percentChance(stats.CritChance.Value);
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
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * stats.MoveSpeed.Value));
    }

    public void PlayerMove(Vector2 target, float moveSpeed)
    {
        oldPosition = transform.position;
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed));
    }

    private void ExitRoom(Direction side)
    {
        controlSM.ChangeState(idle);
    }

    public void OnDestroy()
    {
        EventManager.instance.onDoorwayTriggerEnter -= ExitRoom;
        EventManager.instance.onDealDamage -= ReceiveDamage;
        EventManager.instance.onDealDamage -= DealDamage;
    }

    protected virtual void ReceiveDamage(DamageContext damage)
    {
        if (damage.target == gameObject)
        {
            stats.Health.CurrentValue -= damage.baseDamage;
        }
    }

    protected virtual void DealDamage(DamageContext damage)
    {
        if (damage.source == gameObject)
        {
            Debug.Log("dealt dmg " + damage.baseDamage);
            stats.Health.CurrentValue += damage.baseDamage * stats.LifeSteal.Value;
        }
    }
}
