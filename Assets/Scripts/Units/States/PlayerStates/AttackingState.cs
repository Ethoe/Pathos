using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : BaseState
{
    private bool move;
    private bool fired;
    private bool moving;
    private bool first = true;
    private float attackSpeedTimer;
    private float deltaTime;
    private int autoParam = Animator.StringToHash("AttackBlend");


    public AttackingState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
        ((PlayerStateMachine)stateMachine).WasAttackingState = false;
    }

    public override void Enter()
    {
        base.Enter();
        if (((PlayerStateMachine)stateMachine).target == null || ((PlayerStateMachine)stateMachine).WasAttackingState != true)
        {
            ((PlayerStateMachine)stateMachine).target = GetTarget();
            if (((PlayerStateMachine)stateMachine).target == null)
            {
                stateMachine.ChangeState(player.moving);
                return;
            }
        }
        ((PlayerStateMachine)stateMachine).WasAttackingState = false;

        if (first)
        {
            StartAttack();
            first = false;
        }
        else
        {
            attackSpeedTimer -= Time.time - deltaTime;
            deltaTime = Time.time;
            if (attackSpeedTimer < 0 || !fired)
            {
                StartAttack();
            }
        }
        move = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        move = moveAction.triggered;
        if (attackAction.triggered)
        {
            GameObject nextTarget = GetTarget();
            if (nextTarget == null)
            {
                move = true;
            }
            else
            {
                if (nextTarget != ((PlayerStateMachine)stateMachine).target)
                {
                    ((PlayerStateMachine)stateMachine).target = nextTarget;
                    if (!fired)
                    {
                        StartAttack();
                    }
                }
            }
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (move)
        {
            ((PlayerStateMachine)stateMachine).target = null;
            stateMachine.ChangeState(player.moving);
        }

        attackSpeedTimer -= Time.time - deltaTime;
        deltaTime = Time.time;
        if (player.shoot && !fired)
        {
            player.abilities.Activate(AbilityClass.Attack, ((PlayerStateMachine)stateMachine).target, Vector2.zero, player.layer);
            fired = true;
        }
        if (attackSpeedTimer < 0)
        {
            StartAttack();
        }
    }

    private GameObject GetTarget()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>());
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        GameObject result;

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, player.hit);
        if (hit.collider != null)
        {
            result = hit.collider.gameObject;
        }
        else
        {
            result = AttackBuff(3.0f, mousePos2D);
        }
        return result;
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

    private void StartAttack()
    {
        player.animator.SetFloat("Attack Speed", player.statsComponent.stats.AttackSpeed.Value);
        player.shoot = false;
        if (((PlayerStateMachine)stateMachine).target == null)
        {
            stateMachine.ChangeState(player.idle);
            return;
        }
        if (Vector2.Distance(player.transform.position, ((PlayerStateMachine)stateMachine).target.transform.position) > player.statsComponent.stats.AttackRange.Value)
        {
            stateMachine.ChangeState(player.attackMoving);
            return;
        }
        else
        {
            attackSpeedTimer = Tools.AttackDuration(player.statsComponent.stats.AttackSpeed.Value);
            player.TriggerAnimation(autoParam);
            deltaTime = Time.time;
            fired = false;
        }
    }
}
