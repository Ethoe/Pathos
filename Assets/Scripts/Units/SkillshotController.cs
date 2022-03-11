using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillshotController : MonoBehaviour
{
    // Public Vars

    // Private Vars
    private float lifeSpan = 1.0f;
    private Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
}
