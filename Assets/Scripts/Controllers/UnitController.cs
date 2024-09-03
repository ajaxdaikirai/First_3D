using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : BaseController
{
    //�� �ν� ���� �ð�
    [SerializeField]
    float UpdateLockOnInterval = 0.5f;

    //Ÿ�� �� �Լ� ���� �÷���
    bool _onLockTargetFlag = true;

    public override void Init()
    {
        //���� �ʱ�ȭ
        State = Define.State.Idle;

        //�ִϸ�����
        _anim = gameObject.GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Can't Load Animator Component");
        }

        //������ٵ�
        _rig = gameObject.GetComponent<Rigidbody>();
        if (_anim == null)
        {
            Debug.Log("Can't Load Rigidbody Component");
        }

        //���� �߰�
        _stat = transform.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel($"{gameObject.name}Stat", 1));

        //HP�� �߰�
        if (gameObject.GetComponentInParent<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }

        //�� �ĺ� �ڷ�ƾ ����
        //StartCoroutine(TargetLockCoroutine());


    }


    protected override void UpdateAlways()
    {
        base.UpdateAlways();

        if (Managers.Game.Player == null)
            return;


        //�÷��̾ ������ ����
        if (Managers.Game.Player.GetComponent<PlayerController>().State == Define.State.Die)
            UpdateDie();

        //Ÿ�� ���� �Լ��� ����
        if (_onLockTargetFlag)
        {
            StartCoroutine(TargetLockCoroutine());
        }

        if (Managers.Game.MonsterCrystal == null)
            Despwn();
    }

    protected override void UpdateIdle()
    {
        if (Managers.Game.MonsterCrystal == null || !Managers.Game.MonsterCrystal.activeSelf) return;
        if(_lockTarget != null)
        {
            _onLockTargetFlag = true;
            State = Define.State.Moving;
        }
        if (!_attackFlag) return;
        
    }

    protected override void UpdateMoving()
    {
        //Ÿ���� ���� ��� ������ ����
        if (_lockTarget == null || _lockTarget.activeSelf == false)
        {
            State = Define.State.Idle;
            return;
        }
        //���µ� Ÿ�ٰ��� �Ÿ� ���
        _destPos = _lockTarget.transform.position - transform.position;
        float dis = _destPos.magnitude;
        _dir = _destPos.normalized;
        if(dis < _stat.AttackDistance)
        {
            State = Define.State.Attack;
            return;
        }
        gameObject.GetComponent<NavMeshAgent>().SetDestination(_lockTarget.transform.position);
        //transform.position += _dir * Time.deltaTime * _stat.MoveSpeed;
        if (_dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10 * Time.deltaTime);
    }

    protected override void UpdateAttack()
    {
        //Ÿ���� ���� ��� ������ ����
        if (_lockTarget == null || !_lockTarget.activeSelf)
        {
            if (_lockTarget == Managers.Game.Player)
                State = Define.State.Idle;
            return;
        }

        if ((_lockTarget.transform.position - transform.position).magnitude > _stat.AttackDistance)
        {
            State = Define.State.Moving;
            return;
        }

    }

    protected override void UpdateDie()
    {
        _onLockTargetFlag = false;
        _unitFlag = false;
        StartCoroutine(Despwn());
    }

    void OnAttack()
    {
        _attackFlag = false;
        //Ÿ���� ��Ȱ��ȭ�Ǿ��� ��� ��ŵ
        if (!(_lockTarget == null || !_lockTarget.activeSelf))
        {
            _lockTarget.GetComponent<Stat>().OnAttacked(_stat.Offence);
        }
        
        //���� ��Ÿ��
        StartCoroutine(AttackCoolTime());
    }

    void EndAttack()
    {
        State = Define.State.Idle;
        
    }

    protected IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(_stat.AttackSpeed);
        _attackFlag = true;
    }

    //������ �ð���ŭ Ÿ�� ����
    protected IEnumerator TargetLockCoroutine()
    {
        yield return new WaitForSeconds(UpdateLockOnInterval);
        OnLockTarget();
    }

    //Ÿ�� ���� �Լ�
    protected void OnLockTarget()
    {
        float minDis;
        float distance;

        //ũ����Ż�� ������ �������
        if (Managers.Game.MonsterCrystal == null) return;
        _lockTarget = Managers.Game.MonsterCrystal;
        minDis = (Managers.Game.MonsterCrystal.transform.position - transform.position).sqrMagnitude;

        //�÷��̾� ���� ��� ĳ���Ϳ� �Ÿ��� ���ϰ� ���� ����� ĳ���͸� Ÿ������ ����
        List<GameObject> targetList = Managers.Game.Monsters;
        if (targetList.Count == 0)
        {
            //�⺻ Ÿ������ ũ����Ż�� ����
            _lockTarget = Managers.Game.MonsterCrystal;
            return;
        }
        else
        {
            foreach (GameObject go in targetList)
            {
                distance = (go.transform.position - transform.position).sqrMagnitude;
                if (minDis > distance )
                {
                    _lockTarget = go;
                    minDis = distance;
                    _onLockTargetFlag = false;
                    break;
                }
            }
        }
        
    }

    protected override void UpdateClear()
    {
        State = Define.State.Clear;
        base.UpdateClear();
    }

    protected override IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);
        Managers.Game.Despawn(Define.Layer.Unit, gameObject);
    }
}

