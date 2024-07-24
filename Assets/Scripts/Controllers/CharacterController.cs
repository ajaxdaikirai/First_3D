using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : BaseController
{
    //락온된 오브젝트
    [SerializeField]
    protected GameObject _lockTarget;

    //적 인식 갱신 시간
    [SerializeField]
    protected float _updateLockOnInterval = 2.0f;


    protected override void Init()
    {
        SetCreatureDefault();

        //적 식별 코루틴 실행
        StartCoroutine(TargetLockCoroutine());
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(TargetLockCoroutine());
    }

    protected override void UpdateIdle()
    {
        if (!CanAttackTarget())
            return;

        if (_attackFlag == false)
            return;

        State = Define.State.Moving;
    }

    protected override void UpdateAttack()
    {
        //타겟이 없을 경우 움직임 멈춤
        if (!CanAttackTarget())
        {
            State = Define.State.Idle;
            return;
        }

        if ((_lockTarget.transform.position - transform.position).magnitude > _stat.AttackDistance)
        {
            State = Define.State.Moving;
            return;
        }
    }

    //지정된 시간만큼 타겟 갱신
    protected IEnumerator TargetLockCoroutine()
    {
        while (true)
        {
            OnLockTarget();
            yield return new WaitForSeconds(_updateLockOnInterval);
        }
    }

    // 타겟 갱신 처리
    protected void OnLockTarget()
    {
        float minDis;
        float distance;

        // 기본 타겟으로 크리스탈을 지정
        GameObject mainTarget = MainTarget();
        if (mainTarget == null) return;

        _lockTarget = mainTarget;
        minDis = (_lockTarget.transform.position - transform.position).sqrMagnitude;

        // 플레이어 포함 모든 캐릭터와 거리를 비교하고 가장 가까운 캐릭터를 타겟으로 지정
        List<GameObject> targetList = Targets();
        if (targetList == null || targetList.Count <= 0)
            return;

        foreach (GameObject go in targetList)
        {
            if (go.GetComponent<Stat>().IsDefeated()) 
                continue;

            distance = (go.transform.position - transform.position).sqrMagnitude;
            if (minDis <= distance) continue;

            minDis = distance;
            _lockTarget = go;
        }
    }

    protected override void UpdateMoving()
    {
        //타겟이 없을 경우 움직임 멈춤
        if (_lockTarget == null || _lockTarget.activeSelf == false)
        {
            State = Define.State.Idle;
            return;
        }

        //락온된 타겟과의 거리 계산
        _destPos = _lockTarget.transform.position - transform.position;
        float dis = _destPos.magnitude;
        _dir = _destPos.normalized;
        if (dis < _stat.AttackDistance)
        {
            State = Define.State.Attack;
            return;
        }

        // 이동 처리
        transform.position += _dir * Time.deltaTime * _stat.MoveSpeed;

        // 회전 처리
        if (_dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10 * Time.deltaTime);
    }

    // 공격 처리
    // 애니메이션에서 호출되는 경우가 있음
    void OnAttack()
    {
        _attackFlag = false;
        //타겟이 비활성화되었을 경우 스킵
        if (!(_lockTarget == null || _lockTarget.activeSelf == false))
        {
            _lockTarget.GetComponent<Stat>().OnAttacked(_stat.Offence);
        }

        //공격 쿨타임
        StartCoroutine(AttackCoolTime());
    }

    public override void OnDie()
    {
        if (!_aliveFlag)
            return;

        _aliveFlag = false;
        State = Define.State.Die;

        // 사망 후에는 뒤의 캐릭터에 방해가 되지 않도록 콜라이더를 해제
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        // 사망 처리
        Managers.Game.RemoveFromSpawnList(gameObject);
        StartCoroutine(Despawn());
    }

    //사망시 일정시간 후 비활성화
    protected IEnumerator Despawn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);
        Managers.Resource.Destroy(gameObject);
    }

    // 타겟을 공격할 수 있는가
    protected bool CanAttackTarget()
    {
        if (_lockTarget == null)
            return false;

        if (_lockTarget.activeSelf == false)
            return false;

        // HP가 없는 경우
        if (_lockTarget.GetComponent<Stat>().IsDefeated())
            return false;

        return true;
    }

    // 메인 타겟 
    protected abstract GameObject MainTarget();
    // 메인 이외의 모든 타겟들
    protected abstract List<GameObject> Targets();
}
