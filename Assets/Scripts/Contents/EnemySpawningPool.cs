using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningPool : SpawningPool
{
    protected override void Init()
    {
        base.Init();
        //스폰 지점 초기화
        _spawnPos = Managers.Game.EnemySpawnPos;

        //스폰 액션 추가
        SpawnAction -= AddSqawnAction;
        SpawnAction += AddSqawnAction;
    }

    protected override void AddSqawnAction()
    {
        while (_reserveCount < _keepObjectCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    //예약된 시간 만큼 뒤에 오브젝트 생성
    protected virtual IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTime));
        GameObject enemy = Managers.Game.Spawn(Define.Layer.Enemy, Enum.GetName(typeof(Define.Enemy), Define.Enemy.Enemy));
        enemy.transform.position = _spawnPos;
        enemy.GetComponent<BaseController>().State = Define.State.Idle;
    }

}
