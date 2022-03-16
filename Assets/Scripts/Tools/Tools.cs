using UnityEngine;

public static class Tools
{
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
        return (Random.value < chance);
    }

    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Units/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        return targetFile.text;
    }
}
