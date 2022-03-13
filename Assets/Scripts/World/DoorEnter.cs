using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public Direction side = Direction.Up;
    void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.instance.DoorwayTriggerEnter(side);
    }
}
