using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BuffableEntity : MonoBehaviour
{
    private readonly Dictionary<ScriptableBuff, TimedBuff> _buffs = new Dictionary<ScriptableBuff, TimedBuff>();

    void Start()
    {
        EventManager.StartListening(Events.AddBuffTrigger, AddBuffListener);
    }

    void Update()
    {
        // return before updating each buff if game is paused
        // if (Game.isPaused)
        //    return;

        foreach (var buff in _buffs.Values.ToList())
        {
            buff.Tick(Time.deltaTime);
            if (buff.IsFinished)
            {
                _buffs.Remove(buff.Buff);
            }
        }
    }

    protected void OnDestroy()
    {
        EventManager.StopListening(Events.AddBuffTrigger, AddBuffListener);
    }

    private void AddBuffListener(Dictionary<string, object> message)
    {
        var buff = (BuffContext)message["buff"];
        if (buff.target == gameObject)
        {
            AddBuff(buff.buff);
        }
    }

    public void AddBuff(TimedBuff buff)
    {
        if (_buffs.ContainsKey(buff.Buff))
        {
            _buffs[buff.Buff].Activate();
        }
        else
        {
            _buffs.Add(buff.Buff, buff);
            buff.Activate();
            if (buff.Buff.BuffType == BuffType.ConsumeableBuff)
            {
                RemoveBuff(buff);
            }
        }
    }

    public void RemoveBuff(TimedBuff buff)
    {
        if (_buffs.ContainsKey(buff.Buff))
        {
            _buffs[buff.Buff].End();
        }
    }
}