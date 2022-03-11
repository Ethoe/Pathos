using UnityEngine;
public enum RoomType
{
    FloorStart,
    FloorEnd,
    RegularRoom,
}
public class DungeonRoom
{
    public RoomType type;
    public GameObject layout;
    public Vector2Int location;

    public DungeonRoom(Vector2Int location)
    {
        this.location = location;
    }
}