using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

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
    public GameObject hpBar, leaveRope;
    public DungeonGenerator currentLevel;
    private int difficultyLevel;
    public DungeonRoom currentRoom;
    private Object solidWall, closedGate, openGate;
    public ScriptableEnemyList enemyList;
    public ScriptableItemList itemList;
    private GameObject[] walls; // Goes Up Right Down Left
    private List<Object> solidWalls;
    private WeightedChanceExecutor dropTable;

    private void Awake()
    {
        _instance = this;
        difficultyLevel = 1;
        buildDropTable();
        currentLevel = new DungeonGenerator(7);
        solidWall = LoadWall("Wall");
        closedGate = LoadWall("DoorClosed");
        openGate = LoadWall("DoorOpen");
        walls = new GameObject[4];
        solidWalls = new List<Object>();
        currentRoom = currentLevel.enter;
        var isNewRoom = !currentRoom.visited; // Should always be false

        buildRoom();
        spawnRoom(isNewRoom);
    }

    private void Start()
    {
        EventManager.StartListening(Events.DoorwayTriggerEnter, ExitRoom);
        EventManager.StartListening(Events.ClearedRoomTrigger, ClearRoom);
        EventManager.StartListening(Events.FilledRoomTrigger, CloseRoom);
        EventManager.StartListening(Events.SpawnDropTrigger, AddDrop);
        EventManager.StartListening(Events.EnterLevelTrigger, NextDungeon);
    }

    private void NextDungeon(Dictionary<string, object> message)
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

        if (currentRoom.drops != null)
        {
            foreach (var drop in currentRoom.drops.Reverse<GameObject>())
            {
                if (drop == null)
                    currentRoom.drops.Remove(drop);
                else
                    drop.SetActive(false);
            }
        }
        difficultyLevel++;

        buildDropTable();
        currentLevel.Generate(10); // put in growing number;
        currentRoom = currentLevel.enter;
        var isNewRoom = !currentRoom.visited;
        currentRoom.visited = true;
        EventManager.TriggerEvent(Events.GenerateRoomTrigger, null);
        buildRoom();
        spawnRoom(isNewRoom);
    }

    private void AddDrop(Dictionary<string, object> message)
    {
        if (message["drop"] != null)
        {
            var drop = (GameObject)message["drop"];
            if (drop != null)
                currentRoom.drops.Add(drop);
        }
    }

    private void ClearRoom(Dictionary<string, object> message)
    {
        currentLevel.SetRoomType(currentRoom, RoomType.StandardCleared);
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

    private void CloseRoom(Dictionary<string, object> message)
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
    private void ExitRoom(Dictionary<string, object> message)
    {
        var side = (Direction)message["side"];
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

        if (currentRoom.drops != null && currentRoom.drops.Count > 0)
        {
            foreach (var drop in currentRoom.drops.Reverse<GameObject>())
            {
                if (drop == null)
                    currentRoom.drops.Remove(drop);
                else
                    drop.SetActive(false);
            }
        }

        currentRoom = currentLevel.GetRoom(currentRoom, side);

        var isNewRoom = !currentRoom.visited;
        currentRoom.visited = true;

        EventManager.TriggerEvent(Events.GenerateRoomTrigger, null);
        buildRoom();
        spawnRoom(isNewRoom);

        GameManager.Instance.player.transform.position = GetPlayerSpawn(side);
    }

    private void buildRoom()
    {
        UnityEngine.Object roomWall;
        GameObject room;
        bool setDirection = false;
        if (currentRoom.type != RoomType.Standard)
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
                return Vector2.zero;
        }
    }

    private void spawnRoom(bool isNewRoom)
    {
        switch (currentRoom.type)
        {
            case RoomType.Standard: // Need to refactor to use isNewRoom rather than changing to room.cleared
                for (int count = 0; count < 2; count++)
                {
                    Vector2 spawnLocation = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
                    GameObject enemy = enemyList.Tier1Enemy[Random.Range(0, enemyList.Tier1Enemy.Count)];
                    spawnUnit(enemy, spawnLocation);
                }
                break;
            case RoomType.FloorStart:
                break;
            case RoomType.Boss:
                Instantiate(leaveRope, new Vector3(0, 14, 0), Quaternion.identity);
                currentRoom.type = RoomType.BossCleared;
                break;
            case RoomType.Shop:
                if (isNewRoom)
                {
                    spawnShop();
                }
                break;
            default:
                break;
        }

        if (currentRoom.drops != null && currentRoom.drops.Count > 0)
        {
            foreach (var drop in currentRoom.drops.Reverse<GameObject>())
            {
                if (drop == null)
                    currentRoom.drops.Remove(drop);
                else
                    drop.SetActive(true);
            }
        }
    }

    private void spawnUnit(GameObject unit, Vector2 spawnLocation)
    {
        GameObject currentUnit = Instantiate(unit, spawnLocation, Quaternion.identity);
        GameObject hpUnit = Instantiate(hpBar, spawnLocation, Quaternion.identity);
        hpUnit.GetComponent<LazyHealthBar>().target = currentUnit;
    }

    private void spawnShop()
    {
        //Todo: figure out spawning mechanics better depending on dungeon level (maybe add a new table for weights per level per dungeon floor)
        //Right now only items in tier 1 so only going to be selecting from there
        dropTable.Execute();
    }

    // Call new droptable at the start of each dungeon
    private void buildDropTable()
    {
        dropTable = new WeightedChanceExecutor();
        var itemLists = itemList.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int counter = 1;
        foreach (var prop in itemLists)
        {
            // Todo: update this function to be a spawner with location as input param
            foreach (var item in (List<GameObject>)prop.GetValue(itemList))
            {
                if (item != null)
                {
                    dropTable.AddChance(new WeightedChanceParam(() =>
                            { Instantiate(item, new Vector2(0, 0), Quaternion.identity); }, 1 / (counter * counter))); // Spawn item in center of room with 1/rarity^2 dropweight
                }
            }
            counter++;
        }
    }
}
