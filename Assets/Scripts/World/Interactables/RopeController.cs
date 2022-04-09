using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    private Collider2D collider2d;
    private Vector2 target;
    private bool isLeaving;
    private float LeaveSpeed = 10f;
    void Awake()
    {
        target = Vector2.zero;
        isLeaving = false;
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = false;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, movespeed() * Time.deltaTime);
        if (Mathf.Approximately(Vector2.Distance(transform.position, target), 0))
        {
            collider2d.enabled = true;
        }
        if (Mathf.Approximately(Vector2.Distance(transform.position, target), 0) && isLeaving)
        {
            EventManager.TriggerEvent(Events.EnterLevelTrigger, new Dictionary<string, object> { });
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            isLeaving = true;
            target = new Vector2(0f, 15f);
            EventManager.TriggerEvent(Events.LeaveLevelTrigger, new Dictionary<string, object> { { "speed", LeaveSpeed }, { "target", target } });
        }
    }

    float movespeed()
    {
        if (isLeaving)
            return LeaveSpeed;
        return (2f + (float)Mathf.Sin(Time.time * 6));
    }
}
