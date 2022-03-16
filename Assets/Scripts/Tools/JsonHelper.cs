using UnityEngine;
using System;

/// <summary>
/// How to use
/// var test = new string[2];
/// test[0] = "a";
/// test[1] = "b";
/// string ezJson = JsonHelper.ToJson(test, true);
/// Debug.Log(ezJson);
/// string[] wowie = JsonHelper.FromJson<string>(ezJson);
/// Debug.Log(wowie[1]);
/// </summary>
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}