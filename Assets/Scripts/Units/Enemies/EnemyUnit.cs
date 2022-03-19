using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public StatBlock stats;
    public int difficultyLevel;
    public string statLocation;
    public GameObject ability;
    public StateMachine aiSM;
    public GameObject aggrod;
    public Animator animator;
    protected Rigidbody2D rigidbody2d;
    protected Vector2 oldPosition;

    protected void start()
    {
        GameManager.Instance.AddEnemy(this.gameObject);
        stats = GetComponent<StatBlockComponent>().stats;
        JsonUtility.FromJsonOverwrite(Tools.LoadResourceTextfile(statLocation), stats);
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        oldPosition = this.gameObject.transform.position;
        aiSM = new StateMachine();
        aggrod = GameManager.Instance.player;
    }

    protected void update()
    {
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
}
