using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public  class SpawningPool : MonoBehaviour
{
    //���� �����ϴ� ���� ��
    protected int _unitCount = 0;

    //���� �����ϴ� ���� ��
    protected int _monsterCount = 0;

    //���� ����� ������ ��
    protected int _reserveCount = 0;

    //������ ������ ��
    protected int _keepObjectCount = 0;


    //���� ����� ���� ��
    protected int _reservedMonsterCount = 0;

    //������ ���� ��
    protected int _keepMonsterCount = 0;

    //���庰 �ִ� ���� ��
    protected int _maxMonsterCount = 0;

    //���庰 �ִ� ���� ��
    protected int _maxUnitCount = 0;

    //���� ���
    [SerializeField]
    protected Vector3 _spawnPos;

    //���� ��
    [SerializeField]
    protected float _spawnTime = 0.2f;

    protected const string RESOURCE_ROOT = "Characters/";


    public void AddUnitCount(int value) { _unitCount += value; }
    public void MinusUnitCount(int value) { _unitCount -= value; }
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void MinusMonsterCount(int value) { _monsterCount -= value; }
    public void SetKeepObjectCount(int count) { _keepObjectCount = count; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    public void SetMaxMonsterCount(int count) { _maxMonsterCount = count;  }
    public void SetMaxUnitCount(int count) { _maxUnitCount = count; }

    //ī��Ʈ Ȯ���� ���� ����
    public int GetKeepMonsterCount() { return _keepMonsterCount; }
    public int GetKeepObjectCount() { return _keepObjectCount; }
    public int GetUnitCount() { return _unitCount; }
    public int GetMonsterCount() { return _monsterCount; }
    public int GetMaxMonsterCount() { return _maxMonsterCount; }
    public int GetMaxUnitCount() { return _maxUnitCount; }

    private void Start()
    {
        Init();
    }

    private void Update()
    {


    }

    protected virtual void Init()
    {
    }
}
