using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public StatBlock stats;
    public int difficultyLevel;
    public string statLocation;

    private Rigidbody2D rigidbody2d;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.AddEnemy(this.gameObject);
        stats = GetComponent<StatBlockComponent>().stats;
        JsonUtility.FromJsonOverwrite(Tools.LoadResourceTextfile(statLocation), stats);
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.Health.CurrentValue <= 0)
        {
            GameManager.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    public void UnitMove(Vector2 target, float moveSpeed)
    {
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed));
    }
}
