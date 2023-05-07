using System;

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

    public StatBlock(StatBlockObject statBlock)
    {
        this.Health = new CharacterResource(statBlock.Health.BaseValue);
        this.AttackSpeed = new CharacterStat(statBlock.AttackSpeed.BaseValue);
        this.AttackRange = new CharacterStat(statBlock.AttackRange.BaseValue);
        this.CritChance = new CharacterStat(statBlock.CritChance.BaseValue);
        this.CritDamage = new CharacterStat(statBlock.CritDamage.BaseValue);
        this.CoolDown = new CharacterStat(statBlock.CoolDown.BaseValue);
        this.LifeSteal = new CharacterStat(statBlock.LifeSteal.BaseValue);
        this.Attack = new CharacterStat(statBlock.Attack.BaseValue);
        this.Armor = new CharacterStat(statBlock.Armor.BaseValue);
        this.MoveSpeed = new CharacterStat(statBlock.MoveSpeed.BaseValue);
        this.Invincible = statBlock.Invincible;
        this.isMelee = statBlock.isMelee;
    }
}