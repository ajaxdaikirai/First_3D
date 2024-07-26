using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<key, value>
{
    Dictionary<key, value> MakeDict();
}

public class DataManager
{
    Loader LoadJson<Loader, key, value>(string path) where Loader : ILoader<key, value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Datas/Stats/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public data.Stat GetStatByLevel(string path, int level)
    {
        Dictionary <int, data.Stat> _statDict = LoadJson<data.StatLoader, int, data.Stat>(path).MakeDict();
        if (_statDict == null || _statDict.Count == 0)
        {
            Debug.Log($"Not Exist Json File : Datas/Stats/{path}");
            return null;
        }

        return _statDict[level];
    }
}
