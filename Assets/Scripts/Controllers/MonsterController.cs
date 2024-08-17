using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    //�� �ν� ���� �ð�
    [SerializeField]
    float _updateLockOnInterval = 0.5f;

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

        /*if(Managers.Game.Player.GetComponent<Stat>().Hp == 0)
        {
            State = Define.State.Idle;
            _attackFlag = false;
            _continuedFlag = false;
            return;
        }*/

        if (Managers.Game.MonsterCrystal == null)
            Despwn();

        //Ÿ�� ���� �Լ��� ����
        if (_onLockTargetFlag)
        {
            StartCoroutine(TargetLockCoroutine());
        }
    }

    protected override void UpdateMoving()
    {
        //���µ� Ÿ�ٰ��� �Ÿ� ���
        if (_lockTarget == null || _lockTarget.activeSelf == false)
        {
            State = Define.State.Idle;
            return;
        }

        _destPos = _lockTarget.transform.position - transform.position;
        float dis = _destPos.magnitude;
        _dir = _destPos.normalized;
        _dir.y = 0;
        if (dis < _stat.AttackDistance)
        {
            State = Define.State.Attack;
            return;
        }
        transform.position += _dir * Time.deltaTime * _stat.MoveSpeed;
        if (_dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10 * Time.deltaTime);
    }

    protected override void UpdateIdle()
    {

        if (Managers.Game.Player == null || Managers.Game.Player.activeSelf == false) return;
        if (_lockTarget != null)
        {
            _onLockTargetFlag = true;
            State = Define.State.Moving;
        }
        if (_attackFlag == false) return;

    }

    protected override void UpdateAttack()
    {
        //Ÿ���� ���� ��� ������ ����
        if (_lockTarget == null || _lockTarget.activeSelf == false)
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
    void OnAttack()
    {
        _attackFlag = false;
        GameObject go = _lockTarget;
        //Ÿ���� ��Ȱ��ȭ�Ǿ��� ��� ��ŵ
        if (!(_lockTarget == null || _lockTarget.activeSelf == false))
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

    protected override void UpdateDie()
    {
        _onLockTargetFlag = false;
        _monsterFlag = false;
        StartCoroutine(Despwn());
        //base.UpdateDie();
    }

    //������ �ð���ŭ Ÿ�� ����
    protected IEnumerator TargetLockCoroutine()
    {
        yield return new WaitForSeconds(_updateLockOnInterval);
        OnLockTarget();
    }

    //Ÿ�� ���� �Լ�
    protected void OnLockTarget()
    {
        float minDis;
        float distance;

        //�⺻ Ÿ������ �÷��̾ ����
        if (Managers.Game.Player == null) return;
        _lockTarget = Managers.Game.Player;
        minDis = (Managers.Game.Player.transform.position - transform.position).sqrMagnitude;

        List<GameObject> targetList = Managers.Game.Units;
        
        if (targetList.Count == 0)
        {
            _lockTarget = Managers.Game.Player;
            return;
        }
        else
        {
            foreach (GameObject go in targetList)
            {
                distance = (go.transform.position - transform.position).sqrMagnitude;
                if (minDis > distance)
                {
                    _lockTarget = go;
                    minDis = distance;
                    _onLockTargetFlag = false;
                    break;
                }
            }
        }

        /*if (State != Define.State.Die && _lockTarget.GetComponent<Stat>().Hp != 0) {
            _onLockTargetFlag = true;
        } */
    }

    protected override void UpdateClear()
    {

        State = Define.State.Clear;
        base.UpdateClear();
    }

    protected override IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);
        StopCoroutine(TargetLockCoroutine());
        Managers.Game.Despawn(Define.Layer.Monster, gameObject);
    }
}

