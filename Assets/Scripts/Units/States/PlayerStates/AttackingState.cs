using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : BaseState
{
    private bool move;
    private bool fired;
    private bool hasNext;
    private bool moving;
    private bool first = true;
    private float attackSpeedTimer;
    private float deltaTime;
    private int autoParam = Animator.StringToHash("AttackBlend");


    public AttackingState(PlayerController player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    private void init()
    {
        if (((PlayerStateMachine)stateMachine).target == null)
        {
            stateMachine.ChangeState(player.moving);
            hasNext = true;
            return;
        }
        if (first)
        {
            StartAttack();
            first = false;
        }
        else
        {
            attackSpeedTimer -= Time.time - deltaTime;
            deltaTime = Time.time;
            if (attackSpeedTimer < 0)
            {
                StartAttack();
            }
            else if (fired)
            {
                hasNext = true;
            }
            else
            {
                StartAttack();
            }

        }
        move = false;
    }
    public override void Enter()
    {
        base.Enter();
        if (((PlayerStateMachine)stateMachine).target == null)
        {
            ((PlayerStateMachine)stateMachine).target = GetTarget();
        }
        init();
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
                if (nextTarget == ((PlayerStateMachine)stateMachine).target)
                {
                    hasNext = true;
                }
                else
                {
                    if (!fired)
                    {
                        StartAttack();
                        ((PlayerStateMachine)stateMachine).target = nextTarget;
                    }
                    else if (fired)
                    {
                        hasNext = true;
                        ((PlayerStateMachine)stateMachine).target = nextTarget;
                    }
                }
            }
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.animator.SetFloat("Attack Speed", player.statsComponent.stats.AttackSpeed.Value);

        if (move)
        {
            stateMachine.ChangeState(player.moving);
        }

        attackSpeedTimer -= Time.time - deltaTime;
        deltaTime = Time.time;
        if (player.shoot && !fired)
        {
            player.abilities.Activate(AbilityClass.Attack, ((PlayerStateMachine)stateMachine).target, Vector2.zero, player.layer);
            fired = true;
        }
        if (hasNext && attackSpeedTimer < 0)
        {
            StartAttack();
        }
        else if (attackSpeedTimer < 0)
        {
            StartAttack();
        }
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

    private void StartAttack()
    {
        player.shoot = false;
        if (((PlayerStateMachine)stateMachine).target == null)
        {
            stateMachine.ChangeState(player.idle);
            return;
        }
        if (Vector2.Distance(player.transform.position, ((PlayerStateMachine)stateMachine).target.transform.position) > player.statsComponent.stats.AttackRange.Value)
        {
            stateMachine.ChangeState(player.attackMoving);
        }
        else
        {
            attackSpeedTimer = Tools.AttackDuration(player.statsComponent.stats.AttackSpeed.Value);
            player.TriggerAnimation(autoParam);
            deltaTime = Time.time;
            fired = false;
            hasNext = false;
        }

    }
}
