using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Right,
    Down,
    Left
}

public class DungeonGenerator
{
    public DungeonRoom[,] map;
    public DungeonRoom enter;
    private int size = 16;
    private int tries;
    private int numberRooms;
    private int currentRooms;
    private Queue<DungeonRoom> generatorQueue;

    public DungeonGenerator(int rooms)
    {
        map = new DungeonRoom[size, size]; // Create an empty 16,16 map
        generatorQueue = new Queue<DungeonRoom>();
        numberRooms = rooms;
        currentRooms = 1;
        tries = 0;
        Generate();
    }

    private void Generate()
    {
        map[8, 8] = new DungeonRoom(new Vector2Int(8, 8)); // Create a room in the center lets call it starting room
        enter = map[8, 8];
        enter.type = RoomType.FloorStart;
        tries += 1;
        generatorQueue.Enqueue(enter);

        while (generatorQueue.Count > 0)
        {
            DungeonRoom room = generatorQueue.Dequeue();
            // 1. Determine neighbor cell
            generateNeighbor(room.location + Vector2Int.down);
            generateNeighbor(room.location + Vector2Int.up);
            generateNeighbor(room.location + Vector2Int.left);
            generateNeighbor(room.location + Vector2Int.right);
        }

        if (currentRooms < numberRooms) // Did not generate enough rooms
        {
            if (tries > 5)
                return;
            map = new DungeonRoom[size, size]; // Create an empty 16,16 map
            generatorQueue = new Queue<DungeonRoom>();
            currentRooms = 1;
            Generate();
        }
    }

    private int countNeighbors(Vector2Int location)
    {
        int res = 0;

        if (getRoom(location + Vector2Int.down) != null)
            res += 1;
        if (getRoom(location + Vector2Int.up) != null)
            res += 1;
        if (getRoom(location + Vector2Int.left) != null)
            res += 1;
        if (getRoom(location + Vector2Int.right) != null)
            res += 1;

        return res;
    }

    private DungeonRoom getRoom(Vector2Int location)
    {
        if (0 > location.x || location.x >= size)
            return null;

        if (0 > location.y || location.y >= size)
            return null;

        return map[location.x, location.y];
    }

    private void generateNeighbor(Vector2Int location)
    {
        if (0 > location.x || location.x >= size)
            return;

        if (0 > location.y || location.y >= size)
            return;

        // 2. If room count done, give up (out of order for a bit more effieciency)
        if (currentRooms >= numberRooms)
            return;

        // 3. If has room, give up
        if (getRoom(location) != null)
            return;

        // 4. If has more than 1 neighbor, give up
        if (countNeighbors(location) > 1)
            return;

        // 5. Random chance to give up
        if (Tools.percentChance(.50f))
            return;

        DungeonRoom room = new DungeonRoom(location);
        currentRooms += 1;
        map[location.x, location.y] = room;
        generatorQueue.Enqueue(room);
    }

    public DungeonRoom GetRoom(DungeonRoom room, Direction side)
    {
        switch (side)
        {
            case Direction.Up:
                return getRoom(room.location + Vector2Int.up);
            case Direction.Down:
                return getRoom(room.location + Vector2Int.down);
            case Direction.Left:
                return getRoom(room.location + Vector2Int.left);
            case Direction.Right:
                return getRoom(room.location + Vector2Int.right);
            default:
                return null;
        }
    }
}