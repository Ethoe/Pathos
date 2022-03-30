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
        ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.None;
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
        if (!((PlayerStateMachine)stateMachine).locked)
        {
            if (abilityOneAction.triggered)
                ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.One;
            if (abilityTwoAction.triggered)
                ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.Two;
            if (abilityThreeAction.triggered)
                ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.Three;
            if (abilityFourAction.triggered)
                ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.Four;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!((PlayerStateMachine)stateMachine).locked && ((PlayerStateMachine)stateMachine).UsedAbility != AbilitySwitch.None)
        {
            switch (((PlayerStateMachine)stateMachine).UsedAbility)
            {
                case AbilitySwitch.One:
                    if (player.abilities.Abilities.ContainsKey(AbilityClass.AbilityOne) && player.abilities.Abilities[AbilityClass.AbilityOne].state == AbilityState.ready)
                    {
                        stateMachine.ChangeState(player.ability);
                    }
                    else
                    {
                        ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.None;
                    }
                    break;
                case AbilitySwitch.Two:
                    if (player.abilities.Abilities.ContainsKey(AbilityClass.AbilityTwo) && player.abilities.Abilities[AbilityClass.AbilityTwo].state == AbilityState.ready)
                    {
                        stateMachine.ChangeState(player.ability);
                    }
                    else
                    {
                        ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.None;
                    }
                    break;
                case AbilitySwitch.Three:
                    if (player.abilities.Abilities.ContainsKey(AbilityClass.AbilityThree) && player.abilities.Abilities[AbilityClass.AbilityThree].state == AbilityState.ready)
                    {
                        stateMachine.ChangeState(player.ability);
                    }
                    else
                    {
                        ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.None;
                    }
                    break;
                case AbilitySwitch.Four:
                    if (player.abilities.Abilities.ContainsKey(AbilityClass.AbilityFour) && player.abilities.Abilities[AbilityClass.AbilityFour].state == AbilityState.ready)
                    {
                        stateMachine.ChangeState(player.ability);
                    }
                    else
                    {
                        ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.None;
                    }
                    break;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
