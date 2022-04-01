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

public class DungeonRoom
{
    public RoomType type;
    public GameObject layout;
    public List<GameObject> drops;
    public Vector2Int location;
    public bool visited;

    public DungeonRoom(Vector2Int location)
    {
        drops = new List<GameObject>();
        this.location = location;
    }
}