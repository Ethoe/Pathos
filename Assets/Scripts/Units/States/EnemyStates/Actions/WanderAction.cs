using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Wander")]
public class WanderAction : FSMAction
{
    public override void Execute(BaseStateMachine stateMachine)
    {
        stateMachine.TimeInState -= Time.deltaTime;
        Debug.Log("trying" + stateMachine.TimeInState);
        var movement = stateMachine.GetComponent<MovementController>();

        if (movement.IsMoving)
            return;

        Debug.Log("movin");
        var goingTo = new Vector2(stateMachine.transform.position.x + Random.Range(-5, 5), stateMachine.transform.position.y + Random.Range(-5, 5));
        RaycastHit2D hit = Physics2D.Raycast(goingTo, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Walls"));
        if (hit.collider != null)
        {
            goingTo = new Vector2(stateMachine.transform.position.x + Random.Range(-5, 5), stateMachine.transform.position.y + Random.Range(-5, 5));
            hit = Physics2D.Raycast(goingTo, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Walls"));
        }
        movement.Target = goingTo;
    }

    public override void Enter(BaseStateMachine stateMachine)
    {
        base.Enter(stateMachine);
        stateMachine.TimeInState = Random.Range(4, 8);
    }

    public override void Exit(BaseStateMachine stateMachine)
    {
        var movement = stateMachine.GetComponent<MovementController>();
        movement.activelyMoving = false;
    }
}
