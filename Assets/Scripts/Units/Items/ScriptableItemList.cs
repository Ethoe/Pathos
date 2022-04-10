using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemList")]
public class ScriptableItemList : ScriptableObject
{
    public List<GameObject> Tier1Items;
    public List<GameObject> Tier2Items;
    public List<GameObject> Tier3Items;
    public List<GameObject> Tier4Items;
    public List<GameObject> Tier5Items;
}
