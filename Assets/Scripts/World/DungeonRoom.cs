using UnityEngine;
using System.Collections.Generic;
public enum RoomType
{
    FloorStart,
    FloorEnd,
    Shop,
    Standard,
    StandardCleared,
    Boss,
    BossCleared,
}

public struct RoomDrop
{
    public GameObject Object;
    public Vector2 Location;
}

public class DungeonRoom
{
    public RoomType type;
    public GameObject layout;
    public List<RoomDrop> drops;
    public Vector2Int location;
    public bool visited;

    public DungeonRoom(Vector2Int location)
    {
        this.location = location;
    }
}