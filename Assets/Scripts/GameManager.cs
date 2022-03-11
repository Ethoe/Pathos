using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum DamageDealtType
{
    Physical,
    Magic,
    True,
}

public class GameManager : MonoBehaviour
{
    private List<GameObject> enemies;
    private DungeonGenerator currentLevel;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is dead");
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        enemies = new List<GameObject> { };
        currentLevel = new DungeonGenerator(32);

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

    void Update()
    {

    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public bool RemoveEnemy(GameObject enemy)
    {
        return enemies.Remove(enemy);
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
}
