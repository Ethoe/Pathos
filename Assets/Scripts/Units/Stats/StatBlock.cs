using System;
using UnityEngine;

[CreateAssetMenu(menuName = "StatBlock")]
[Serializable]
public class StatBlock : ScriptableObject
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
}