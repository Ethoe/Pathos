using System.Collections;
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
    public GameObject autoAttackProjectile;
    public GameObject skillShotProjectile;
    public StatBlock stats;
    public bool shoot;

    // Private Vars
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        shoot = false;
        controlSM = new StateMachine();
        idle = new IdleState(this, controlSM);
        moving = new MovingState(this, controlSM);
        attacking = new AttackingState(this, controlSM);
        attackMoving = new AttackMovingState(this, controlSM);
        controlSM.Initialize(idle);
        rigidbody2d = GetComponent<Rigidbody2D>();

        stats = GetComponent<StatBlockComponent>().stats;
        stats.Attack.BaseValue = 100.0f;
        stats.CritChance.BaseValue = .5f;
        stats.CritDamage.BaseValue = 1.0f;
        stats.AttackSpeed.BaseValue = 1.0f;
        stats.AttackRange.BaseValue = 3.0f;
        stats.MoveSpeed.BaseValue = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        controlSM.CurrentState.HandleInput();
        controlSM.CurrentState.LogicUpdate();

        animator.SetFloat("AttackSpeed", stats.AttackSpeed.Value);
    }

    void FixedUpdate()
    {
        controlSM.CurrentState.PhysicsUpdate();
    }

    public void AutoAttack(GameObject target)
    {
        GameObject autoAttack = Instantiate(autoAttackProjectile, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        ProjectileController projectile = autoAttack.GetComponent<ProjectileController>();
        projectile.target = target;
        projectile.source = this.gameObject;
        projectile.isCrit = Tools.percentChance(stats.CritChance.Value);
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
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * stats.MoveSpeed.Value));
    }
}
