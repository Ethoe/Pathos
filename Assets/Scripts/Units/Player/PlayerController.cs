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
    public PlayerAbilityState ability;
    public bool shoot;
    public int layer = 10; //Physics collider level ally projectile
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
    [HideInInspector]
    public bool playerInControl;
    [HideInInspector]
    public AbilityHolder abilities;
    public MovementController movement;
    private Collider2D collider2d;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        EventManager.StartListening(Events.DoorwayTriggerEnter, ExitRoom);
        EventManager.StartListening(Events.AbilityAnimationTrigger, TriggerAnimationListener);
        EventManager.StartListening(Events.LeaveLevelTrigger, LeaveLevelListener);
        EventManager.StartListening(Events.EnterLevelTrigger, EnterLevelListener);

        playerInControl = true;

        buffBar = gameObject.GetComponent<BuffableEntity>();

        abilities = GetComponent<AbilityHolder>();
        movement = GetComponent<MovementController>();

        collider2d = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        shoot = false;

        playerInput = GetComponent<PlayerInput>();
        controlSM = new PlayerStateMachine();
        idle = new IdleState(this, controlSM);
        moving = new MovingState(this, controlSM);
        attacking = new AttackingState(this, controlSM);
        attackMoving = new AttackMovingState(this, controlSM);
        ability = new PlayerAbilityState(this, controlSM);
        controlSM.Initialize(idle);

        spriteRenderer = GetComponent<SpriteRenderer>();

        statsComponent = GetComponent<StatBlockComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moving = movement.Direction;
        animator.SetFloat("Move X", moving.x);
        animator.SetFloat("Move Y", moving.y);

        if (playerInControl)
        {
            controlSM.CurrentState.HandleInput();
            controlSM.CurrentState.LogicUpdate();
        }
    }

    void FixedUpdate()
    {
        if (playerInControl)
        {
            controlSM.CurrentState.PhysicsUpdate();
        }
    }

    public void AutoAttackFire()
    {
        shoot = true;
    }

    public void TriggerAnimation(int param)
    {
        animator.Play(param, 0, 0.0f);
    }

    private void TriggerAnimationListener(Dictionary<string, object> message)
    {
        var source = (GameObject)message["source"];
        var param = (int)message["param"];
        if (source == gameObject)
        {
            animator.Play(param, 0, 0.0f);
        }
    }

    private void LeaveLevelListener(Dictionary<string, object> message)
    {
        controlSM.ChangeState(idle);
        playerInControl = false;
        movement.Target = (Vector2)message["target"];
        movement.MoveSpeed = (float)message["speed"];
        collider2d.enabled = false;
        // TODO: remember to remove movespeed active and turn on collider
    }

    private void EnterLevelListener(Dictionary<string, object> message)
    {
        playerInControl = true;
        movement.activelyMoving = false;
        movement.SetMoveSpeed = false;
        transform.position = new Vector3(0, 0, 0);
        collider2d.enabled = true;
    }

    private void ExitRoom(Dictionary<string, object> message)
    {
        controlSM.ChangeState(idle);
    }

    public void OnDestroy()
    {
        EventManager.StopListening(Events.DoorwayTriggerEnter, ExitRoom);
        EventManager.StopListening(Events.AbilityAnimationTrigger, TriggerAnimationListener);
        EventManager.StopListening(Events.LeaveLevelTrigger, LeaveLevelListener);
        EventManager.StopListening(Events.EnterLevelTrigger, EnterLevelListener);
    }
}
