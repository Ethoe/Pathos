using UnityEngine;
using System;

[Serializable]
public class CharacterResource : CharacterStat
{
    public float CurrentValue;

    public CharacterResource(float currentValue) : base()
    {
        BaseValue = currentValue;
        CurrentValue = currentValue;
    }

    public CharacterResource() : this(0) { }
}