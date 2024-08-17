using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawningPool : SpawningPool
{
    public Vector3 monsterPos;
    private IEnumerator spawnCoroutine;
    //����
    protected Stat _stat;
    protected int _summonedMonsterCount;
    protected override void Init()
    {
        base.Init();
        _summonedMonsterCount = 0;
        //���� ���� �ʱ�ȭ
        //_spawnPos = Managers.Game.MonsterSpawnPos;

    }

    void Update()
    {
        
        if (_reservedMonsterCount + _monsterCount <= _keepMonsterCount)
        {
            if (_maxMonsterCount == _summonedMonsterCount)
            {
                StopCoroutine(ReserveSpawn());
                return; 
            }
            StartCoroutine(ReserveSpawn());
        }
        
    }

    //����� �ð� ��ŭ �ڿ� ������Ʈ ����
    protected virtual IEnumerator ReserveSpawn()
    {
        
        _reservedMonsterCount++;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0, _spawnTime));

        //ũ����Ż ������ ���� �ٸ� �� ��ȯ�ϰ�ʹ�
        int cs = Conf.Main.CURRENT_STAGE;
        GameObject monster;
        switch (cs)
        {
            case 1:
                monster = Managers.Game.Spawn(Define.Layer.Monster, "Monsters/Green");
                break;
            case 2:
                monster = Managers.Game.Spawn(Define.Layer.Monster, "Monsters/Blue");
                break;
            case 3:
                monster = Managers.Game.Spawn(Define.Layer.Monster, "Monsters/Purple");
                break;
            case 4:
                monster = Managers.Game.Spawn(Define.Layer.Monster, "Monsters/Red");
                break;

        }
        Managers.Game.CreatePos(Define.Layer.Monster);

        _reservedMonsterCount--;
        _summonedMonsterCount++;
    }


}
