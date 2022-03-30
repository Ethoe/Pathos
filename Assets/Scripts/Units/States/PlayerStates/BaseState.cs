using UnityEngine;
using UnityEngine.InputSystem;

public class BaseState : State
{
    protected PlayerController player;
    public InputAction moveAction;
    public InputAction attackAction;
    public InputAction abilityOneAction;
    public InputAction abilityTwoAction;
    public InputAction abilityThreeAction;
    public InputAction abilityFourAction;
    public InputAction mousePosition;

    public BaseState(PlayerController player, PlayerStateMachine stateMachine) : base(stateMachine)
    {
        this.player = player;
        this.stateMachine = (PlayerStateMachine)stateMachine;
        moveAction = player.playerInput.actions["Move"];
        attackAction = player.playerInput.actions["Attack"];
        abilityOneAction = player.playerInput.actions["AbilityOne"];
        abilityTwoAction = player.playerInput.actions["AbilityTwo"];
        abilityThreeAction = player.playerInput.actions["AbilityThree"];
        abilityFourAction = player.playerInput.actions["AbilityFour"];
        mousePosition = player.playerInput.actions["MousePosition"];
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.animator.SetFloat("Attack Speed", player.statsComponent.stats.AttackSpeed.Value);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
