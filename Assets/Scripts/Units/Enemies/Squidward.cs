using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squidward : MonoBehaviour
{
    public StateMachine aiSM;
    public StatBlock stats;
    public EnemyUnit baseUnit;
    public SquidwardWanderState wander;
    public SquidwardIdleState idle;
    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Start()
    {
        aiSM = new StateMachine();
        wander = new SquidwardWanderState(this, aiSM);
        idle = new SquidwardIdleState(this, aiSM);
        aiSM.Initialize(wander);

        stats = GetComponent<StatBlockComponent>().stats;
        baseUnit = GetComponent<EnemyUnit>();
        rigidbody2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        aiSM.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        aiSM.CurrentState.PhysicsUpdate();
    }

    public void UnitMove(Vector2 target, float moveSpeed)
    {
        rigidbody2d.MovePosition(Vector2.MoveTowards(transform.position, target, Time.deltaTime * moveSpeed));
    }
}
