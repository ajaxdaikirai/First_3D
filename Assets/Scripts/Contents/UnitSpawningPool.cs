using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawningPool : SpawningPool
{  
    private IEnumerator spawnCoroutine;
    protected override void Init()
    {
        base.Init();
        //���� ���� �ʱ�ȭ
        _spawnPos = Managers.Game.UnitSpawnPos;
        spawnCoroutine = ReserveSpawn();

    }

    void Update()
    {
        if (_reserveCount + _unitCount <= _keepObjectCount)
        {
            //StartCoroutine(ReserveSpawn());
        }
        else
        {
            StopCoroutine(ReserveSpawn());
        }
    }


    //����� �ð� ��ŭ �ڿ� ������Ʈ ����
    protected virtual IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTime));
        GameObject character = Managers.Game.Spawn(Define.Layer.Unit, "Units/FitnessGirlSniper");
        character.transform.position = Managers.Game.CreatePos(Define.Layer.Unit);

        _reserveCount--;
    }

}
