using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action<Direction> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(Direction side)
    {
        if (onDoorwayTriggerEnter != null)
            onDoorwayTriggerEnter(side);
    }

    public event Action onClearedRoom;
    public void ClearedRoomTrigger()
    {
        if (onClearedRoom != null)
            onClearedRoom();
    }

    public event Action onGenerateRoom;
    public void GenerateRoomTrigger()
    {
        if (onGenerateRoom != null)
            onGenerateRoom();
    }

    public event Action onFilledRoom;
    public void FilledRoomTrigger()
    {
        if (onFilledRoom != null)
            onFilledRoom();
    }

    public event Action<DamageContext> onDealDamage;
    public void DealDamageTrigger(DamageContext context)
    {
        if (onDealDamage != null)
            onDealDamage(context);
    }

    public event Action<Vector2> onPlayerClick;
    public void PlayerClick(Vector2 location)
    {
        if (onPlayerClick != null)
            onPlayerClick(location);
    }

    public event Action onPlayerExitMove;
    public void PlayerExitMove()
    {
        if (onPlayerExitMove != null)
            onPlayerExitMove();
    }

    public event Action<GameObject> onSpawnDrop;
    public void SpawnDrop(GameObject gameobject)
    {
        if (onSpawnDrop != null)
            onSpawnDrop(gameobject);
    }
}
