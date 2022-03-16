using System.Collections.Generic;
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
    public GameObject player;
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
    }

    void Start()
    {

    }

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public bool RemoveEnemy(GameObject enemy)
    {
        bool res = enemies.Remove(enemy);
        if (enemies.Count <= 0)
        {
            EventManager.instance.ClearedRoomTrigger();
        }
        return res;
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void CalculateDamage(GameObject source, GameObject target, GameObject damageSource, bool isCrit)
    {
        StatBlock sourceStats = source.GetComponent<StatBlockComponent>().stats;
        StatBlock targetStats = target.GetComponent<StatBlockComponent>().stats;
        CharacterStat calculator = new CharacterStat(sourceStats.Attack.Value);
        if (isCrit)
        {
            calculator.AddModifier(new StatModifier(sourceStats.CritDamage.Value, StatModType.PercentMult));
        }
        calculator.AddModifier(new StatModifier(Tools.ResistDamageMultiplier(targetStats.Armor.Value), StatModType.PercentMult));
        targetStats.Health.CurrentValue -= calculator.Value;
        EventManager.instance.DealDamageTrigger(target, new DamageInfo(calculator.Value, DamageDealtType.Physical, isCrit));
    }
}
