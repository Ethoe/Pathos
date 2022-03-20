using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyUnit
{

    public SlimeWanderState wander;
    public SlimeAttackState attack;
    // Start is called before the first frame update
    void Start()
    {
        this.start();
        abilityCooldown = 5.0f;
        wander = new SlimeWanderState(this, aiSM);
        attack = new SlimeAttackState(this, aiSM);
        aiSM.Initialize(attack);
    }

    // Update is called once per frame
    void Update()
    {
        this.update();
        aiSM.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        this.fixedUpdate();
        aiSM.CurrentState.PhysicsUpdate();
    }

    public void Ability()
    {
        GameObject projectileObject = Instantiate(ability, rigidbody2d.position, Quaternion.identity);
        PointAbilityController projectile = projectileObject.GetComponent<PointAbilityController>();
        projectile.owner = this.gameObject;
        projectile.duration = .5f;
    }

}
