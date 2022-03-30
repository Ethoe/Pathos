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
    AbiltyOne,
    AbiltyTwo,
    AbiltyThree,
    AbiltyFour
}

public class AbilityHolder : MonoBehaviour
{
    public ScriptableAbility Attack;
    public ScriptableAbility AbiltyOne;
    public ScriptableAbility AbiltyTwo;
    public ScriptableAbility AbiltyThree;
    public ScriptableAbility AbiltyFour;

    public Dictionary<AbilityClass, TimedAbility> Abilities = new Dictionary<AbilityClass, TimedAbility>();

    void Start()
    {
        if (Attack != null)
            Abilities.Add(AbilityClass.Attack, Attack.InitializeAbility(gameObject));
        if (AbiltyOne != null)
            Abilities.Add(AbilityClass.AbiltyOne, AbiltyOne.InitializeAbility(gameObject));
        if (AbiltyTwo != null)
            Abilities.Add(AbilityClass.AbiltyTwo, AbiltyTwo.InitializeAbility(gameObject));
        if (AbiltyThree != null)
            Abilities.Add(AbilityClass.AbiltyThree, AbiltyThree.InitializeAbility(gameObject));
        if (AbiltyFour != null)
            Abilities.Add(AbilityClass.AbiltyFour, AbiltyFour.InitializeAbility(gameObject));
    }

    void Update()
    {
        foreach (var ability in Abilities.Values.ToList())
        {
            ability.Tick(Time.deltaTime);
        }
    }

    public void Activate(AbilityClass ability, GameObject target, Vector2 direction)
    {
        if (Abilities.ContainsKey(ability))
        {
            Abilities[ability].Activate(target, direction);
        }
    }
}