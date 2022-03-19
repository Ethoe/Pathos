using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyUnit
{

    public SlimeWanderState wander;
    // Start is called before the first frame update
    void Start()
    {
        this.start();
        wander = new SlimeWanderState(this, aiSM);
        aiSM.Initialize(wander);
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

    public void Ability(Vector2 direction)
    {
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90.0f;
        GameObject projectileObject = Instantiate(ability, rigidbody2d.position, Quaternion.AngleAxis(angle, Vector3.forward));
        SkillshotController projectile = projectileObject.GetComponent<SkillshotController>();
        projectile.owner = this.gameObject;
        projectile.range = 10.0f;
        projectile.Launch(direction, 10 * 50); // 50 = 1 unit per second
    }

}
