using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    Vector2 target;
    bool isLeaving;
    float LeaveSpeed = 10f;
    void Awake()
    {
        target = Vector2.zero;
        isLeaving = false;
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, movespeed() * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggered");
        if (other.gameObject == GameManager.Instance.player)
        {
            // Probably want to trigger something
            // EventManager.TriggerEvent(Events.DoorwayTriggerEnter, new Dictionary<string, object> { { "side", side } });
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
