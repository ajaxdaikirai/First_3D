using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    //GameManager이름을 사용할 수 없는 관계로 Ex를 붙임

    //플레이어
    GameObject _player;

    //적 넥서스
    GameObject _enemyCore;

    //스폰 되는 지점
    Vector3 _characterSpawnPos;
    Vector3 _enemySpawnPos;

    //스폰되어 있는 캐릭터
    List<GameObject> _characters = new List<GameObject>();
    //스폰되어 있는 적
    List<GameObject> _enemies = new List<GameObject>();

    public GameObject Player { get { return _player; } }
    public GameObject EnemyCore { get { return _enemyCore; } }
    public List<GameObject> Characters { get { return _characters; } }
    public List<GameObject> Enemies { get { return _enemies; } }

    //스폰 되는 지점
    public Vector3 CharacterSpawnPos { get { return _characterSpawnPos; } }
    public Vector3 EnemySpawnPos { get { return _enemySpawnPos; } }

    public void Init()
    {
        GameObject CharacterSpawnSpot = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.UnitSpawnSpot));
        if (CharacterSpawnSpot == null)
        {
            Debug.Log("Failed Load UnitSpawnSpot");
            return;
        }
        _characterSpawnPos = CharacterSpawnSpot.transform.position;

        GameObject EnemySpawnSpot = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.MonsterSpawnSpot));
        if (CharacterSpawnSpot == null)
        {
            Debug.Log("Failed Load MonsterSpawnSpot");
            return;
        }
        _enemySpawnPos = EnemySpawnSpot.transform.position;

        GameObject EnemyCore = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.MonsterCrystal));
        if (EnemyCore == null)
        {
            Debug.Log("Failed Load MonsterCrystal");
            return;
        }
        _enemyCore = EnemyCore;
    }

    public GameObject InstantiatePlayer()
    {
        GameObject player = Managers.Resource.Instantiate("Characters/Player");
        if(player == null)
        {
            Debug.Log("Failed Load Player");
            return null;
        }
        _player = player;
        player.transform.position = _characterSpawnPos;

        return player;
    }

    public GameObject Spawn(Define.Layer layer, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate($"Characters/{path}", parent);

        switch (layer)
        {
            case Define.Layer.Character:
                _characters.Add(go);
                break;
            case Define.Layer.Enemy:
                _enemies.Add(go);
                break;
        }

        return go;
    }

    public void Despawn(Define.Layer layer, GameObject go)
    {
        switch (layer)
        {
            case Define.Layer.Character:
                if (_characters.Contains(go))
                {
                    _characters.Remove(go);
                }
                else
                {
                    Debug.Log($"Not Spawned Object ObjectName:{go.name}, Layer{Enum.GetName(typeof(Define.Layer), layer)}");
                    return;
                }
                break;
            case Define.Layer.Enemy:
                if (_enemies.Contains(go))
                {
                    _enemies.Remove(go);
                }
                else
                {
                    Debug.Log($"Not Spawned Object ObjectName:{go.name}, Layer{Enum.GetName(typeof(Define.Layer), layer)}");
                    return;
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
