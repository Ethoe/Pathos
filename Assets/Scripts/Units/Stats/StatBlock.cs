using System;

[Serializable]
public class StatBlock
{
    public CharacterResource Health;
    public CharacterStat HealthRegen;
    public CharacterResource Mana;
    public CharacterStat ManaRegen;
    public CharacterStat AttackSpeed;
    public CharacterStat AttackRange;
    public CharacterStat CritChance;
    public CharacterStat CritDamage;
    public CharacterStat CoolDown;
    public CharacterStat LifeSteal;
    public CharacterStat Attack;
    public CharacterStat Magic;
    public CharacterStat MArmor;
    public CharacterStat Armor;
    public CharacterStat MoveSpeed;
    public StatBlock()
    {
        Health = new CharacterResource();
        Mana = new CharacterResource();
        HealthRegen = new CharacterStat();
        ManaRegen = new CharacterStat();
        AttackSpeed = new CharacterStat();
        AttackRange = new CharacterStat();
        CritChance = new CharacterStat();
        CritDamage = new CharacterStat();
        CoolDown = new CharacterStat();
        LifeSteal = new CharacterStat();
        Attack = new CharacterStat();
        Magic = new CharacterStat();
        MArmor = new CharacterStat();
        Armor = new CharacterStat();
        MoveSpeed = new CharacterStat();
    }
}