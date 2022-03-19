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
        if (enemies.Count <= 0)
        {
            EventManager.instance.FilledRoomTrigger();
        }
        enemies.Add(enemy);
    }

    public void CalculateDamage(GameObject source, GameObject target, float damageSource, bool isCrit)
    {
        if (!source || !target) // If source or target is gone on hit no damage, is this right?
            return;

        StatBlockComponent sourceC = source.GetComponent<StatBlockComponent>();
        StatBlockComponent targetC = target.GetComponent<StatBlockComponent>();

        // TODO: expand this

        if (!sourceC || !targetC)
            return;

        StatBlock sourceStats = sourceC.stats;
        StatBlock targetStats = targetC.stats;

        CharacterStat calculator = new CharacterStat(damageSource);
        if (isCrit)
        {
            calculator.AddModifier(new StatModifier(sourceStats.CritDamage.Value, StatModType.PercentMult));
        }
        calculator.AddModifier(new StatModifier(Tools.ResistDamageMultiplier(targetStats.Armor.Value), StatModType.PercentMult));
        targetStats.Health.CurrentValue -= calculator.Value;
        EventManager.instance.DealDamageTrigger(target, new DamageInfo(calculator.Value, DamageDealtType.Physical, isCrit));
    }
}
