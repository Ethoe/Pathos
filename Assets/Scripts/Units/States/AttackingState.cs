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
    private GameObject target;
    private float attackSpeedTimer;
    private float deltaTime;
    private int autoParam = Animator.StringToHash("AutoAttack");


    public AttackingState(PlayerController player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    private void init()
    {
        if (target == null)
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
        target = GetTarget();
        init();
    }

    public override void Enter(GameObject param)
    {
        base.Enter(param);
        target = param;
        init();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        move = Input.GetKeyDown(KeyCode.Mouse1);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject nextTarget = GetTarget();
            if (nextTarget == null)
            {
                move = true;
            }
            else
            {
                if (nextTarget == target)
                {
                    hasNext = true;
                }
                else
                {
                    if (!fired)
                    {
                        StartAttack();
                        target = nextTarget;
                    }
                    else if (fired)
                    {
                        hasNext = true;
                        target = nextTarget;
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
            stateMachine.ChangeState(player.moving);
        }

        attackSpeedTimer -= Time.time - deltaTime;
        deltaTime = Time.time;
        if (player.shoot && !fired)
        {
            player.AutoAttack(target);
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
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
        if (Vector2.Distance(player.transform.position, target.transform.position) > player.stats.AttackRange.Value)
        {
            stateMachine.ChangeState(player.attackMoving, target);
        }
        else
        {
            attackSpeedTimer = Tools.AttackDuration(player.stats.AttackSpeed.Value);
            player.TriggerAnimation(autoParam);
            deltaTime = Time.time;
            fired = false;
            hasNext = false;
        }

    }
}
