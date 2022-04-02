using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableObject : MonoBehaviour
{
    void Start()
    {
        EventManager.TriggerEvent(Events.SpawnDropTrigger, new Dictionary<string, object> { { "drop", gameObject } });
    }
}
