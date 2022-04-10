using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    private BuffableEntity buffBar;
    public readonly Dictionary<TimedBuff, int> items;
    public void AddItem(ScriptableBuff scriptableBuff)
    {
        var buff = scriptableBuff.InitializeBuff(gameObject);
        if (items.ContainsKey(buff))
        {
            buffBar.AddBuff(buff);
            items[buff]++;
        }
        else
        {
            buffBar.AddBuff(buff);
            items.Add(buff, 1);
        }
    }
}
