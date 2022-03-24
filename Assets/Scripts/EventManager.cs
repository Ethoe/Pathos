using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;
    }

    public delegate void DoorwayEnterAction(Direction side);
    public event DoorwayEnterAction onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(Direction side)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(side);
        }
    }

    public delegate void ClearedRoomAction();
    public event ClearedRoomAction onClearedRoom;
    public void ClearedRoomTrigger()
    {
        if (onClearedRoom != null)
        {
            onClearedRoom();
        }
    }

    public delegate void FilledRoomAction();
    public event FilledRoomAction onFilledRoom;
    public void FilledRoomTrigger()
    {
        if (onFilledRoom != null)
        {
            onFilledRoom();
        }
    }

    public delegate void DealDamageAction(DamageContext context);
    public event DealDamageAction onDealDamage;
    public void DealDamageTrigger(DamageContext context)
    {
        if (onDealDamage != null)
        {
            onDealDamage(context);
        }
    }
}
