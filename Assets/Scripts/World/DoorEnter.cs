using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Direction side = Direction.Up;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == GameManager.Instance.player)
        {
            EventManager.TriggerEvent(Events.DoorwayTriggerEnter, new Dictionary<string, object> { { "side", side } });
        }
    }
}
