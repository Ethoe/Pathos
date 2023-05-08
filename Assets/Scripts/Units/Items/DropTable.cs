using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DropTable")]
public class DropTable : ScriptableObject
{
    public int Rolls;
    public List<ItemDrop> Drops;
}
