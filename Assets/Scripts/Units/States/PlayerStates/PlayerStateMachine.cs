using UnityEngine;
using System.Collections.Generic;

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
    private GameObject _target;
    public GameObject target
    {
        get
        {
            if (_target != null)
            {
                if (!_target.activeInHierarchy)
                {
                    _target = null;
                }
            }
            return _target;
        }
        set
        {
            EventManager.TriggerEvent(Events.PlayerEndTargettedTrigger, new Dictionary<string, object> { { "target", target } });
            _target = value;
            EventManager.TriggerEvent(Events.PlayerTargettedTrigger, new Dictionary<string, object> { { "target", target } });
        }
    }
    public AbilitySwitch UsedAbility;
    public bool WasAttackingState;
}
