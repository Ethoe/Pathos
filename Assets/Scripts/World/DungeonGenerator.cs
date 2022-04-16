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
    private RoomType[] roomsToGenerate = new RoomType[] { RoomType.FloorEnd, RoomType.Shop };
    public DungeonRoom[,] map;
    public DungeonRoom enter;
    private int size = 16;
    private int numberRooms;
    private int currentRooms;
    private Queue<DungeonRoom> generatorQueue;
    private List<DungeonRoom> deadEnds;

    public DungeonGenerator(int rooms)
    {
        Generate(rooms);
    }

    public void Generate(int targetRooms)
    {
        CleanDungeon();
        map = new DungeonRoom[size, size]; // Create an empty 16,16 map
        generatorQueue = new Queue<DungeonRoom>();
        deadEnds = new List<DungeonRoom>();
        currentRooms = 1;
        numberRooms = targetRooms;
        map[8, 8] = new DungeonRoom(new Vector2Int(8, 8)); // Create a room in the center lets call it starting room
        enter = map[8, 8];
        enter.type = RoomType.FloorStart;
        enter.visited = true;
        generatorQueue.Enqueue(enter);


        while (generatorQueue.Count > 0)
        {
            DungeonRoom room = generatorQueue.Dequeue();
            // 1. Determine neighbor cell
            int neighbors = 0;
            neighbors += generateNeighbor(room.location + Vector2Int.down);
            neighbors += generateNeighbor(room.location + Vector2Int.up);
            neighbors += generateNeighbor(room.location + Vector2Int.left);
            neighbors += generateNeighbor(room.location + Vector2Int.right);
            if (neighbors == 0)
            {
                deadEnds.Add(room);
            }
        }

        if (currentRooms < numberRooms) // Did not generate enough rooms
        {
            map = new DungeonRoom[size, size]; // Create an empty 16,16 map
            generatorQueue = new Queue<DungeonRoom>();
            deadEnds = new List<DungeonRoom>();
            currentRooms = 1;
            Generate(targetRooms);
        }
        else
        {
            DecideRooms();
        }
    }

    private void DecideRooms()
    {
        Tools.Shuffle(deadEnds);
        var count = 0;
        foreach (var room in deadEnds)
        {
            if (deadEnds.Count > count)
            {
                room.type = roomsToGenerate[count];
                count++;
            }
        }
        // Debug.Log(deadEnds);
        // if (deadEnds.Count >= 1)
        // {
        //     var randomNumb = Random.Range(0, deadEnds.Count - 1);
        //     deadEnds[randomNumb].type = RoomType.FloorEnd;
        //     deadEnds.RemoveAt(randomNumb);
        // }

        // // // if (deadEnds.Count >= 1)
        // // // {
        // // //     var randomNumb = Random.Range(0, deadEnds.Count - 1);
        // // //     deadEnds[randomNumb].type = RoomType.Shop;
        // // //     deadEnds.RemoveAt(randomNumb);
        // // // }
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

    private int generateNeighbor(Vector2Int location)
    {
        if (0 > location.x || location.x >= size)
            return 0;

        if (0 > location.y || location.y >= size)
            return 0;

        // 2. If room count done, give up (out of order for a bit more effieciency)
        if (currentRooms >= numberRooms)
            return 0;

        // 3. If has room, give up
        if (getRoom(location) != null)
            return 0;

        // 4. If has more than 1 neighbor, give up
        if (countNeighbors(location) > 1)
            return 0;

        // 5. Random chance to give up
        if (Tools.percentChance(.50f))
            return 0;

        DungeonRoom room = new DungeonRoom(location);
        room.type = RoomType.Standard;
        currentRooms += 1;
        map[location.x, location.y] = room;
        generatorQueue.Enqueue(room);
        return 1;
    }

    public DungeonRoom GetRoom(DungeonRoom room, Direction side)
    {
        switch (side)
        {
            case Direction.Up:
                return getRoom(room.location + new Vector2Int(-1, 0));
            case Direction.Down:
                return getRoom(room.location + new Vector2Int(1, 0));
            case Direction.Left:
                return getRoom(room.location + new Vector2Int(0, -1));
            case Direction.Right:
                return getRoom(room.location + new Vector2Int(0, 1));
            default:
                return null;
        }
    }

    public void SetRoomType(DungeonRoom room, RoomType type)
    {
        Vector2Int location = room.location;
        map[location.x, location.y].type = type;
    }

    private void CleanDungeon()
    {
        if (map == null)
            return;

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] != null)
                {
                    foreach (var drop in map[x, y].drops)
                    {
                        Object.Destroy(drop);
                    }
                }
            }
        }
    }
}