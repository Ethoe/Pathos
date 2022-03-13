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
    private DungeonGenerator currentLevel;
    private DungeonRoom currentRoom;
    private Object solidWall, closedGate, openGate;
    private GameObject[] walls; // Goes Up Right Down Left
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
        BuildRoom();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < currentLevel.map.GetLength(1); i++)
        {
            for (int j = 0; j < currentLevel.map.GetLength(0); j++)
            {
                if (currentLevel.map[i, j] != null)
                    sb.Append("x");
                else
                    sb.Append('_');
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }

    private void Start()
    {
        EventManager.instance.onDoorwayTriggerEnter += ExitRoom;
        EventManager.instance.onClearedRoom += ClearRoom;
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
    private void ExitRoom(Direction side)
    {
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

        GameManager.Instance.player.transform.position = GetPlayerSpawn(side);

        EventManager.instance.ClearedRoomTrigger();

        Debug.Log("wowo on " + side);
    }

    private void BuildRoom()
    {
        if (currentLevel.GetRoom(currentRoom, Direction.Up) != null)
        {
            walls[0] = (GameObject)Instantiate(closedGate, new Vector2(-1, 8), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            walls[0] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(-1, 8), Quaternion.Euler(0, 0, 0)));
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Right) != null)
        {
            walls[1] = (GameObject)Instantiate(closedGate, new Vector2(9, -1), Quaternion.Euler(0, 0, 270));
        }
        else
        {
            walls[1] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(9, -1), Quaternion.Euler(0, 0, 270)));
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Down) != null)
        {
            walls[2] = (GameObject)Instantiate(closedGate, new Vector2(0, -11), Quaternion.Euler(0, 0, 180));
        }
        else
        {
            walls[2] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(0, -11), Quaternion.Euler(0, 0, 180)));
        }

        if (currentLevel.GetRoom(currentRoom, Direction.Left) != null)
        {
            walls[3] = (GameObject)Instantiate(closedGate, new Vector2(-10, -2), Quaternion.Euler(0, 0, 90));
        }
        else
        {
            walls[3] = null;
            solidWalls.Add(Instantiate(solidWall, new Vector2(-10, -2), Quaternion.Euler(0, 0, 90)));
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
                return new Vector2(0, -9);
            case Direction.Right:
                return new Vector2(-8, -1);
            case Direction.Down:
                return new Vector2(0, 6);
            case Direction.Left:
                return new Vector2(7, -1);
            default:
                return new Vector2();
        }
    }
}
