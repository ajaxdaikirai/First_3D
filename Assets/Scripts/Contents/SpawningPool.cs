using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawningPool : MonoBehaviour
{
    //생성 예약된 오브젝트
    protected int _reserveCount = 0;

    //유지할 오브젝트의 수
    [SerializeField]
    protected int _keepObjectCount = 0;

    //스폰 장소
    [SerializeField]
    protected Vector3 _spawnPos;

    //스폰 텀
    [SerializeField]
    protected float _spawnTime = 5.0f;

    protected const string RESOURCE_ROOT = "Characters/";

    public void SetKeepEnemyCount(int count) { _keepObjectCount = count; }

    //스폰 엑션
    protected Action SpawnAction = null;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (SpawnAction == null) return;
        SpawnAction.Invoke();
    }

    //추가할 액션
    protected abstract void AddSqawnAction();

    protected virtual void Init()
    {
    }
}
