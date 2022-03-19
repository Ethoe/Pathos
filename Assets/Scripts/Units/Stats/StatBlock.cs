using System;
using UnityEngine;

[Serializable]
public class StatBlock
{
    public CharacterResource Health;
    public CharacterStat AttackSpeed;
    public CharacterStat AttackRange;
    public CharacterStat CritChance;
    public CharacterStat CritDamage;
    public CharacterStat CoolDown;
    public CharacterStat LifeSteal;
    public CharacterStat Attack;
    public CharacterStat Armor;
    public CharacterStat MoveSpeed;
    public bool Invincible;
    public bool isMelee;
    public StatBlock()
    {
        Health = new CharacterResource();
        AttackSpeed = new CharacterStat();
        AttackRange = new CharacterStat();
        CritChance = new CharacterStat();
        CritDamage = new CharacterStat();
        CoolDown = new CharacterStat();
        LifeSteal = new CharacterStat();
        Attack = new CharacterStat();
        Armor = new CharacterStat();
        MoveSpeed = new CharacterStat();
    }

    public string getJson()
    {
        return JsonUtility.ToJson(this);
    }
}