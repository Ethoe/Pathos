using System.Collections.Generic;
using UnityEngine;

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

    void Start() { }

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

    public void CalculateDamage(DamageContext context)
    {
        if (!context.source || !context.target) // If source or target is gone on hit no damage, is this right?
            return;

        StatBlockComponent sourceC = context.source.GetComponent<StatBlockComponent>();
        StatBlockComponent targetC = context.target.GetComponent<StatBlockComponent>();

        // TODO: expand this

        if (!sourceC || !targetC)
            return;

        StatBlock sourceStats = sourceC.stats;
        StatBlock targetStats = targetC.stats;

        CharacterStat calculator = new CharacterStat();
        if (context.type == DamageDealtType.Basic)
        {
            calculator.BaseValue = sourceStats.Attack.Value;
        }
        else
        {
            calculator.BaseValue = context.baseDamage;
        }

        if (context.isCrit)
        {
            calculator.AddModifier(new StatModifier(sourceStats.CritDamage.Value, StatModType.PercentMult));
        }
        calculator.AddModifier(new StatModifier(Tools.ResistDamageMultiplier(targetStats.Armor.Value), StatModType.PercentMult));

        EventManager.instance.DealDamageTrigger(new DamageContext(context.source, context.target, calculator.Value, context.isCrit, context.type));
    }
}
