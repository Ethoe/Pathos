using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyList")]
public class ScriptableEnemyList : ScriptableObject
{
    public List<GameObject> Tier1Enemy;
    public List<GameObject> Tier2Enemy;
    public List<GameObject> Tier3Enemy;
    public List<GameObject> Tier4Enemy;
    public List<GameObject> Bosses;
}
