using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableAbility : ScriptableObject
{
    public string Name;
    public float Cooldown;
    public float CastTime;
    public float Range;
    public GameObject ability;

    public abstract TimedAbility InitializeAbility(GameObject source);
}
