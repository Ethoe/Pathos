using UnityEngine;
using System.Text;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    private static RoomManager _instance;
    public static RoomManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is dead");
            return _instance;
        }
    }
    public GameObject hpBar;
    private DungeonGenerator currentLevel;
    private DungeonRoom currentRoom;
    private Object solidWall, closedGate, openGate;
    private GameObject[] walls; // Goes Up Right Down Left
    private object[] enemies;
    private List<Object> solidWalls;

    private void Awake()
    {
        _instance = this;
        currentLevel = new DungeonGenerator(10);
        solidWall = LoadWall("Wall");
        closedGate = LoadWall("DoorClosed");
        openGate = LoadWall("DoorOpen");
        walls = new GameObject[4];
        solidWalls = new List<Object>();
        currentRoom = currentLevel.enter;
        enemies = Resources.LoadAll("Units/Objects/Enemies");

        BuildRoom();
        spawnRoom();

        /*
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < currentLevel.map.GetLength(1); i++)
                {
                    for (int j = 0; j < currentLevel.map.GetLength(0); j++)
                    {
                        if (currentLevel.map[i, j] != null)
                            sb.Append(currentLevel.map[i, j].location);
                        else
                            sb.Append("(0, 0)");
                    }
                    sb.AppendLine();
                }
                Debug.Log(sb.ToString());
        */
    }

    private void Start()
    {
        EventManager.instance.onDoorwayTriggerEnter += ExitRoom;
        EventManager.instance.onClearedRoom += ClearRoom;
        EventManager.instance.onFilledRoom += CloseRoom;
    }

    private void ClearRoom()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] != null)
            {
                GameObject gate = (GameObject)Instantiate(openGate, walls[i].transform.position, walls[i].transform.rotation);
                gate.GetComponentInChildren<DoorEnter>().side = (Direction)i; // give direction to the door
                Destroy(walls[i]);
                walls[i] = gate;
            }
        }
    }

    private void CloseRoom()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] != null)
            {
                GameObject gate = (GameObject)Instantiate(closedGate, walls[i].transform.position, walls[i].transform.rotation);
                Destroy(walls[i]);
                walls[i] = gate;
            }
        }
    }
    private void ExitRoom(Direction side)
    {
        Debug.Log(side);
        for (int i = 0; i < walls.Length; i++)
        {
            if (walls[i] != null)
            {
                Destroy(walls[i]);
                walls[i] = null;
            }
        }

        for (int i = solidWalls.Count - 1; i >= 0; i--)
        {
            Destroy(solidWalls[i]);
            solidWalls.RemoveAt(i);
        }

        currentRoom = currentLevel.GetRoom(currentRoom, side);

        BuildRoom();

        spawnRoom();

        GameManager.Instance.player.transform.position = GetPlayerSpawn(side);
    }

    private void BuildRoom()
    {
        UnityEngine.Object roomWall;
        GameObject room;
        bool setDirection = false;
        if (currentRoom.type != RoomType.RegularRoom)
        {
            roomWall = openGate;
            setDirection = true;
        }
        else
        {
            roomWall = closedGate;
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Up) != null)
        {
            room = (GameObject)Instantiate(roomWall, new Vector2(0, 9), Quaternion.Euler(0, 0, 0));
            walls[0] = room;
            if (setDirection)
                room.GetComponentInChildren<DoorEnter>().side = Direction.Up;
        }
        else
        {
            walls[0] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(0, 9), Quaternion.Euler(0, 0, 0)));
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Right) != null)
        {
            room = (GameObject)Instantiate(roomWall, new Vector2(9, 1), Quaternion.Euler(0, 0, 270));
            walls[1] = room;
            if (setDirection)
                room.GetComponentInChildren<DoorEnter>().side = Direction.Right;
        }
        else
        {
            walls[1] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(9, 1), Quaternion.Euler(0, 0, 270)));
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Down) != null)
        {
            room = (GameObject)Instantiate(roomWall, new Vector2(1, -8), Quaternion.Euler(0, 0, 180));
            walls[2] = room;
            if (setDirection)
                room.GetComponentInChildren<DoorEnter>().side = Direction.Down;
        }
        else
        {
            walls[2] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(1, -8), Quaternion.Euler(0, 0, 180)));
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Left) != null)
        {
            room = (GameObject)Instantiate(roomWall, new Vector2(-8, 0), Quaternion.Euler(0, 0, 90));
            walls[3] = room;
            if (setDirection)
                room.GetComponentInChildren<DoorEnter>().side = Direction.Left;
        }
        else
        {
            walls[3] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(-8, 0), Quaternion.Euler(0, 0, 90)));
        }
    }

    private UnityEngine.Object LoadWall(string filename)
    {
        return Resources.Load("Sides/" + filename);
    }

    private Vector2 GetPlayerSpawn(Direction side)
    {
        switch (side)
        {
            case Direction.Up:
                return new Vector2(1, -7);
            case Direction.Right:
                return new Vector2(-7, 1);
            case Direction.Down:
                return new Vector2(1, 8);
            case Direction.Left:
                return new Vector2(8, 1);
            default:
                return new Vector2();
        }
    }

    private void spawnRoom()
    {
        if (currentRoom.type == RoomType.FloorEnd || currentRoom.type == RoomType.FloorStart)
        {
            //EventManager.instance.ClearedRoomTrigger();
            return;
        }
        for (int count = 0; count < 1; count++)
        {
            Vector2 spawnLocation = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
            GameObject enemy = (GameObject)enemies[Random.Range(0, enemies.Length)];
            spawnUnit(enemy, spawnLocation);
        }
    }

    private void spawnUnit(GameObject unit, Vector2 spawnLocation)
    {
        GameObject currentUnit = Instantiate(unit, spawnLocation, Quaternion.identity);
        GameObject hpUnit = Instantiate(hpBar, spawnLocation, Quaternion.identity);
        hpUnit.GetComponent<LazyHealthBar>().target = currentUnit;
    }
}
