using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Minimap : MonoBehaviour
{
    public GameObject mask, StandardRoom, currentRoom;
    private List<GameObject> rooms;
    void Start()
    {
        rooms = new List<GameObject>();
        EventManager.StartListening(Events.GenerateRoomTrigger, loadMinimap);
        GameObject newRoom = Instantiate<GameObject>(currentRoom);
        newRoom.transform.SetParent(gameObject.transform);
        newRoom.transform.localPosition = new Vector2(0, 0);
        loadMinimap(null);
    }

    void OnDestroy()
    {
        EventManager.StopListening(Events.GenerateRoomTrigger, loadMinimap);
    }

    void loadMinimap(Dictionary<string, object> message)
    {
        Vector2 roomLoc = RoomManager.Instance.currentRoom.location;
        DungeonRoom[,] map = RoomManager.Instance.currentLevel.map;

        foreach (var room in rooms)
        {
            Destroy(room);
        }
        rooms.Clear();

        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] != null && map[x, y].visited)
                {
                    GameObject newRoom = Instantiate<GameObject>(StandardRoom);
                    newRoom.transform.SetParent(mask.transform);
                    newRoom.transform.localPosition = Quaternion.Euler(0, 0, -90) * ((new Vector2(x, y) - roomLoc) * 25);
                    rooms.Add(newRoom);
                }
            }
        }
    }
}
