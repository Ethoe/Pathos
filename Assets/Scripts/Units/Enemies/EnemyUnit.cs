using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public StatBlock stats;
    public float abilityTimer;
    public int difficultyLevel;
    public int layer = 8; //Physics collider level enemy projectile
    public GameObject ability;
    public StateMachine aiSM;
    public Animator animator;
    protected Rigidbody2D rigidbody2d;
    protected Vector2 oldPosition;

    [HideInInspector]
    public AbilityHolder abilityHolder;
    [HideInInspector]
    public GameObject aggrod;


    void Start()
    {
        start();
    }
    protected void start()
    {
        stats = GetComponent<StatBlockComponent>().stats;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        abilityHolder = GetComponent<AbilityHolder>();
        aiSM = new StateMachine();
        oldPosition = this.gameObject.transform.position;
        aggrod = GameManager.Instance.player;
    }

    void Update()
    {
        update();
    }
    protected void update()
    {
        abilityTimer -= Time.deltaTime;
        if (animator != null)
        {
            Vector2 moving = ((Vector2)this.gameObject.transform.position - oldPosition).normalized;
            animator.SetFloat("Move X", moving.x);
            animator.SetFloat("Move Y", moving.y);
        }
    }

    protected void fixedUpdate() { }

    public void UnitMove(Vector2 target, float moveSpeed)
    {
        oldPosition = transform.position;
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed));
    }

    public void TriggerAnimation(int param)
    {
        animator.Play(param, 0, 0.0f);
    }
}
