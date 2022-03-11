using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected PlayerController player;
    protected StateMachine stateMachine;

    protected State(PlayerController player, StateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void Enter(GameObject param)
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
