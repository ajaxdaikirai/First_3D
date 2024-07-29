using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<key, value>
{
    Dictionary<key, value> MakeDict();
}

public class DataManager
{
    const string ROOT_DIRECTORY = "Datas/";
    const string STAT_DIRECTORY = ROOT_DIRECTORY + "Stats/";
    const string STAGE_DIRECTORY = ROOT_DIRECTORY + "Stages/";

    public string GetJsonText(string path)
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(path);
        return textAsset.text;
    }

    Loader LoadJson<Loader, key, value>(string path) where Loader : ILoader<key, value>
    {
        return JsonUtility.FromJson<Loader>(GetJsonText(path));
    }

    public data.Stat GetStatByLevel(string path, int level)
    {
        Dictionary <int, data.Stat> _statDict = LoadJson<data.StatLoader, int, data.Stat>(STAT_DIRECTORY + path).MakeDict();
        if (_statDict == null || _statDict.Count == 0)
        {
            Debug.Log($"Not Exist Json File : {STAT_DIRECTORY + path}");
            return null;
        }

        return _statDict[level];
    }

    // 스테이지ID로 스테이지를 취득
    public data.Stage GetStageByStageId(int stageId)
    {
        data.Stage stage = JsonUtility.FromJson<data.Stage>(GetJsonText(STAGE_DIRECTORY + stageId));

        if (stage == null)
        {
            Debug.Log($"Not Exist Json File : {STAGE_DIRECTORY + stageId}");
            return null;
        }

        return stage;
    }

    // 스테이지ID로 스테이지 몬스터 정보를 취득
    public List<data.StageSpawnMonster> GetStageSpawnMonsterByStageId(int stageId)
    {
        data.Stage stage = GetStageByStageId(stageId);
        return stage.spawn_monsters;
    }
}
