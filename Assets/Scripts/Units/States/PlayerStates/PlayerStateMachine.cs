using UnityEngine;

public enum AbilitySwitch
{
    One,
    Two,
    Three,
    Four,
    None
}
public class PlayerStateMachine : StateMachine
{
    public bool locked;
    public GameObject target;
    public AbilitySwitch UsedAbility;
}
