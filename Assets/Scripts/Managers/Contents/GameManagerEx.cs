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
    GameObject _monsterCrystal;

    //스폰 되는 지점
    Vector3 _unitSpawnPos;
    Vector3 _monsterSpawnPos;

    //스폰위치 랜덤 범위
    float _positionVar = 1.0f;

    //스폰되어 있는 캐릭터
    List<GameObject> _units = new List<GameObject>();
    //스폰되어 있는 적
    List<GameObject> _monsters = new List<GameObject>();

    public GameObject Player { get { return _player; } }
    public GameObject MonsterCrystal { get { return _monsterCrystal; } }
    public List<GameObject> Units { get { return _units; } }
    public List<GameObject> Monsters { get { return _monsters; } }

    //스폰 되는 지점
    public Vector3 UnitSpawnPos { get { return _unitSpawnPos; } }
    public Vector3 MonsterSpawnPos { get { return _monsterSpawnPos; } }

    public void Init()
    {
        GameObject unitSpawnPos = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.UnitSpawnSpot));
        if (unitSpawnPos == null)
        {
            Debug.Log("Failed Load UnitSpawnSpot");
            return;
        }
        _unitSpawnPos = unitSpawnPos.transform.position;

        GameObject monsterSpawnSpot = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.MonsterSpawnSpot));
        if (monsterSpawnSpot == null)
        {
            Debug.Log("Failed Load MonsterSpawnSpot");
            return;
        }
        _monsterSpawnPos = monsterSpawnSpot.transform.position;

        GameObject monsterCrystal = GameObject.Find(Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.MonsterCrystal));
        if (monsterCrystal == null)
        {
            Debug.Log("Failed Load MonsterCrystal");
            return;
        }
        _monsterCrystal = monsterCrystal;
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
        
        player.transform.position = _unitSpawnPos;

        return player;
    }

    //캐릭터가 생성되는 위치를 반환
    public Vector3 CreatePos(int layer)
    {
        Vector3 basePos;
        
        switch (layer)
        {
            case (int)Define.Layer.Unit:
                basePos = _unitSpawnPos;
                break;
            case (int)Define.Layer.Monster:
                basePos = _monsterSpawnPos;
                break;
            default:
                Debug.Log($"Undifned Case : Layer {Enum.GetName(typeof(Define.SceneLocateObject), Define.SceneLocateObject.MonsterCrystal)}");
                return Vector3.zero;
        }

        //랜덤 위치 생성
        Vector3 newPos = new Vector3(
            UnityEngine.Random.Range(basePos.x - _positionVar, basePos.x + _positionVar),
            basePos.y,
            UnityEngine.Random.Range(basePos.z - _positionVar, basePos.z + _positionVar)
        );

        return newPos;
    }

    public GameObject Spawn(int layer, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate($"Characters/{path}", parent);

        switch (layer)
        {
            case (int)Define.Layer.Unit:
                _units.Add(go);
                break;
            case (int)Define.Layer.Monster:
                _monsters.Add(go);
                break;
        }
        
        //위치 설정
        go.transform.position = CreatePos(layer);

        return go;
    }

    public void Despawn(int layer, GameObject go)
    {
        switch (layer)
        {
            case (int)Define.Layer.Unit:
                if (_units.Contains(go))
                {
                    _units.Remove(go);
                }
                else
                {
                    Debug.Log($"Not Spawned Object ObjectName:{go.name}, Layer{Enum.GetName(typeof(Define.Layer), layer)}");
                    return;
                }
                break;
            case (int)Define.Layer.Monster:
                if (_monsters.Contains(go))
                {
                    _monsters.Remove(go);
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
