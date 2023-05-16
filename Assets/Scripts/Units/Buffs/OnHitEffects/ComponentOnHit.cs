using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ComponentOnHit : MonoBehaviour
{
    private readonly Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier> _sourceDamageModifiers = new Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier>();
    private readonly Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier> _targetDamageModifiers = new Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier>();
    private readonly Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents> _targetDamageEvents = new Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents>();
    private readonly Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents> _sourceDamageEvents = new Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents>();

    // Modifiers for giving damage
    public void AddSourceModifier(ObjectOnHitModifier modifier)
    {
        addModifier(modifier, _sourceDamageModifiers);
    }

    // Modifiers for receiving damage
    public void AddTargetModifier(ObjectOnHitModifier modifier)
    {
        addModifier(modifier, _sourceDamageModifiers);
    }
    // Modifiers for giving damage
    public List<StatModifier> GetSourceModifiers(DamageContext context)
    {
        return listModifiers(_sourceDamageModifiers, context);
    }

    // Modifiers for receiving damage
    public List<StatModifier> GetTargetModifiers(DamageContext context)
    {
        return listModifiers(_sourceDamageModifiers, context);
    }

    // Modifiers for giving damage
    public void RemoveSourceModifier(ObjectOnHitModifier modifier)
    {
        removeModifier(modifier, _sourceDamageModifiers);
    }

    // Modifiers for receiving damage
    public void RemoveTargetModifier(ObjectOnHitModifier modifier)
    {
        removeModifier(modifier, _sourceDamageModifiers);
    }

    private void addModifier(ObjectOnHitModifier modifier, Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier> dict)
    {
        if (dict.ContainsKey(modifier.Modifier))
        {
            dict[modifier.Modifier].Activate();
        }
        else
        {
            dict.Add(modifier.Modifier, modifier);
            modifier.Activate();
        }
    }

    private List<StatModifier> listModifiers(Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier> dict, DamageContext context)
    {
        List<StatModifier> mods = new List<StatModifier>();
        foreach (var mod in dict.Values.ToList())
        {
            mods.Add(mod.GetStatModifier(context));
        }
        return mods;
    }

    private void removeModifier(ObjectOnHitModifier modifier, Dictionary<ScriptableOnHitModifier, ObjectOnHitModifier> dict)
    {
        if (dict.ContainsKey(modifier.Modifier))
        {
            dict[modifier.Modifier].End();
            dict.Remove(modifier.Modifier);
        }
    }



    // Events for giving damage
    public void AddSourceEvent(ObjectOnHitEvents eventO)
    {
        addEvent(eventO, _sourceDamageEvents);
    }
    // Events for receiving damage
    public void AddTargetEvent(ObjectOnHitEvents eventO)
    {
        addEvent(eventO, _targetDamageEvents);
    }
    // Events for giving damage
    public void TriggerSourceEvent(DamageContext context)
    {
        triggerEvents(_sourceDamageEvents, context);
    }
    // Events for receiving damage
    public void TriggerTargetEvent(DamageContext context)
    {
        triggerEvents(_targetDamageEvents, context);
    }
    // Events for giving damage
    public void RemoveSourceEvent(ObjectOnHitEvents eventO)
    {
        removeEvent(eventO, _sourceDamageEvents);
    }
    // Events for receiving damage
    public void RemoveTargetEvent(ObjectOnHitEvents eventO)
    {
        removeEvent(eventO, _targetDamageEvents);
    }

    private void addEvent(ObjectOnHitEvents eventO, Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents> dict)
    {
        if (dict.ContainsKey(eventO.EventSO))
        {
            dict[eventO.EventSO].Activate();
        }
        else
        {
            dict.Add(eventO.EventSO, eventO);
            eventO.Activate();
        }
    }

    private void triggerEvents(Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents> dict, DamageContext context)
    {
        foreach (var eventO in dict.Values.ToList())
        {
            eventO.EventTrigger(context);
        }
    }

    private void removeEvent(ObjectOnHitEvents eventO, Dictionary<ScriptableOnHitEvents, ObjectOnHitEvents> dict)
    {
        if (dict.ContainsKey(eventO.EventSO))
        {
            dict[eventO.EventSO].End();
            dict.Remove(eventO.EventSO);
        }
    }
}
