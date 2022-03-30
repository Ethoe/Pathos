using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityState
{
    ready,
    casting,
    cooldown
}

public enum AbilityClass
{
    Attack,
    AbilityOne,
    AbilityTwo,
    AbilityThree,
    AbilityFour
}

public class AbilityHolder : MonoBehaviour
{
    public ScriptableAbility Attack;
    public ScriptableAbility AbilityOne;
    public ScriptableAbility AbilityTwo;
    public ScriptableAbility AbilityThree;
    public ScriptableAbility AbilityFour;

    public Dictionary<AbilityClass, TimedAbility> Abilities = new Dictionary<AbilityClass, TimedAbility>();

    void Start()
    {
        if (Attack != null)
            Abilities.Add(AbilityClass.Attack, Attack.InitializeAbility(gameObject));
        if (AbilityOne != null)
            Abilities.Add(AbilityClass.AbilityOne, AbilityOne.InitializeAbility(gameObject));
        if (AbilityTwo != null)
            Abilities.Add(AbilityClass.AbilityTwo, AbilityTwo.InitializeAbility(gameObject));
        if (AbilityThree != null)
            Abilities.Add(AbilityClass.AbilityThree, AbilityThree.InitializeAbility(gameObject));
        if (AbilityFour != null)
            Abilities.Add(AbilityClass.AbilityFour, AbilityFour.InitializeAbility(gameObject));
    }

    void Update()
    {
        foreach (var ability in Abilities.Values.ToList())
        {
            ability.Tick(Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        foreach (var ability in Abilities.Values.ToList())
        {
            ability.LogicTick(Time.deltaTime);
        }
    }

    public void Activate(AbilityClass ability, GameObject target, Vector2 direction, int layer)
    {
        if (Abilities.ContainsKey(ability))
        {
            Abilities[ability].Activate(target, direction, layer);
        }
    }
}