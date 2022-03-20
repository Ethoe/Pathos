using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public StatBlock stats;
    public float abilityTimer;
    public int difficultyLevel;
    public string statLocation;
    public GameObject ability;
    public StateMachine aiSM;
    public GameObject aggrod;
    public Animator animator;
    protected Rigidbody2D rigidbody2d;
    protected Vector2 oldPosition;
    protected float abilityCooldown;

    protected void start()
    {
        GameManager.Instance.AddEnemy(this.gameObject);
        stats = GetComponent<StatBlockComponent>().stats;
        JsonUtility.FromJsonOverwrite(Tools.LoadResourceTextfile(statLocation), stats);
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        aiSM = new StateMachine();
        oldPosition = this.gameObject.transform.position;
        aggrod = GameManager.Instance.player;
    }

    protected void update()
    {
        abilityTimer -= Time.deltaTime;
        if (stats.Health.CurrentValue <= 0)
        {
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
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

    public float AbilityCooldown()
    {
        return abilityCooldown - (abilityCooldown * stats.CoolDown.Value);
    }

    public void TriggerAnimation(int param)
    {
        animator.Play(param, 0, 0.0f);
    }
}
