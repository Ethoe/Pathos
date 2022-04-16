using UnityEngine;
using System;
using System.Collections.Generic;

public static class Tools
{
    static System.Random _random = new System.Random(((int)Time.realtimeSinceStartup));

    public static float AttackDuration(float attackSpeed)
    {
        return 1.0f / attackSpeed;
    }

    public static float CompletionPercent(float total, float remaining)
    {
        return 1.0f - (remaining / total);
    }

    public static float ResistDamageMultiplier(float resist)
    {
        if (resist >= 0)
        {
            return (1 - (100 / (100 + resist))) * -1;
        }
        else
        {
            return 2 - (100 / (100 - resist));
        }
    }

    /// <summary>
    /// Returns the percent chance of returning true.
    /// </summary>
    public static bool percentChance(float chance)
    {
        return (UnityEngine.Random.value < chance);
    }

    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Units/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        return targetFile.text;
    }

    public static void Shuffle<T>(List<T> array)
    {
        int n = array.Count;
        for (int i = 0; i < (n - 1); i++)
        {
            // Use Next on random instance with an argument.
            // ... The argument is an exclusive bound.
            //     So we will not go past the end of the array.
            int r = i + _random.Next(n - i);
            T t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }
}
