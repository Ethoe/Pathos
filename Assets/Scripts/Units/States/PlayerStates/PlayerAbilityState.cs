using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : BaseState
{
    public PlayerAbilityState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        ((PlayerStateMachine)stateMachine).locked = true;
        GameObject target = GetTarget();
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
        switch (((PlayerStateMachine)stateMachine).UsedAbility)
        {
            case AbilitySwitch.One:
                player.abilities.Activate(AbilityClass.AbilityOne, target, mousePos, player.layer);
                break;
            case AbilitySwitch.Two:
                player.abilities.Activate(AbilityClass.AbilityTwo, target, mousePos, player.layer);
                break;
            case AbilitySwitch.Three:
                player.abilities.Activate(AbilityClass.AbilityThree, target, mousePos, player.layer);
                break;
            case AbilitySwitch.Four:
                player.abilities.Activate(AbilityClass.AbilityFour, target, mousePos, player.layer);
                break;
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        stateMachine.ChangeState(player.idle);
    }

    public override void Exit()
    {
        base.Exit();
        ((PlayerStateMachine)stateMachine).UsedAbility = AbilitySwitch.None;
        ((PlayerStateMachine)stateMachine).locked = false;
    }

    private GameObject GetTarget()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, player.hit);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return AttackBuff(3.0f, mousePos2D);
        }
    }

    private GameObject AttackBuff(float range, Vector2 clickLocation)
    {
        float closest = Mathf.Infinity;
        GameObject res = null;
        foreach (GameObject enemy in GameManager.Instance.GetEnemies())
        {
            float distance = Vector2.Distance(enemy.transform.position, clickLocation);
            if (distance < closest)
            {
                res = enemy;
                closest = distance;
            }
        }
        if (closest > range)
        {
            return null;
        }
        return res;
    }
}
