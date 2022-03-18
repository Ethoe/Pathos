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
    protected Rigidbody2D rigidbody2d;
    public GameObject aggrod;

    protected void start()
    {
        GameManager.Instance.AddEnemy(this.gameObject);
        stats = GetComponent<StatBlockComponent>().stats;
        JsonUtility.FromJsonOverwrite(Tools.LoadResourceTextfile(statLocation), stats);
        rigidbody2d = GetComponent<Rigidbody2D>();
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
    }

    protected void fixedUpdate() { }

    public void UnitMove(Vector2 target, float moveSpeed)
    {
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed));
    }
}
