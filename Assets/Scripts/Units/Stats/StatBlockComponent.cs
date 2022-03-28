using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBlockComponent : MonoBehaviour
{
    public StatBlock originalStats;
    public StatBlock stats;

    void OnEnable()
    {
        stats = Instantiate<StatBlock>(originalStats);
    }
}
