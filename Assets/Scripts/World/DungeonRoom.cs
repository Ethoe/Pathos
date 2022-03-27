using UnityEngine;
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
    public Vector2Int location;
    public bool visited;

    public DungeonRoom(Vector2Int location)
    {
        this.location = location;
    }
}