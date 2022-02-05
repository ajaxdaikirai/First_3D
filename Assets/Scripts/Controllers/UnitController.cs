using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : BaseController
{
    //스텟
    [SerializeField]
    Stat _stat;

    //적 인식 갱신 시간
    [SerializeField]
    float _updateLockOnInterval = 2.0f;

    //타겟 락 함수 실행 플래그
    bool _onLockTargetFlag = false;

    public override void Init()
    {
        //스텟 추가
        _stat = transform.GetComponent<Stat>();

        //HP바 추가
        if (gameObject.GetComponentInParent<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }

        //적 식별 코루틴 실행
        StartCoroutine(TargetLockCoroutine());
    }

    protected override void UpdateAlways()
    {
        //타겟 락온 함수를 실행
        if (_onLockTargetFlag == false)
        {
            _onLockTargetFlag = true;
            StartCoroutine(TargetLockCoroutine());
        }
    }

    protected override void UpdateMoving()
    {
        //락온된 타겟과의 거리 계산
        if (_lockTarget == null || _lockTarget.activeSelf == false)
        {
            State = Define.State.Idle;
            return;
        }

        Vector3 lockTargetPos = _lockTarget.transform.position;
        lockTargetPos.y = transform.position.y;
        _destPos = lockTargetPos - transform.position;

    }

    protected override void UpdateIdle()
    {
        if (_lockTarget == null || _lockTarget.activeSelf == false) return;
        State = Define.State.Moving;
    }

    protected override void UpdateAttack()
    {
        State = Define.State.Idle;
    }

    protected override void UpdateDie()
    {
        _onLockTargetFlag = false;
    }

    //지정된 시간만큼 타겟 갱신
    protected IEnumerator TargetLockCoroutine()
    {
        OnLockTarget();
        yield return new WaitForSeconds(_updateLockOnInterval);
        if (_lockTarget != null || _lockTarget.activeSelf)
        {
            Debug.Log($"Locked Target:{_lockTarget.name}");
        }
        _onLockTargetFlag = false;
    }

    //타겟 갱신 함수
    protected void OnLockTarget()
    {
        float minDis;
        float distance;

        //기본 타겟으로 플레이어를 지정
        if (Managers.Game.EnemyCore == null) return;
        _lockTarget = Managers.Game.EnemyCore;
        minDis = (Managers.Game.EnemyCore.transform.position - transform.position).sqrMagnitude;

        //플레이어 포함 모든 캐릭터와 거리를 비교하고 가장 가까운 캐릭터를 타겟으로 지정
        List<GameObject> targetList = Managers.Game.Monsters;
        if (targetList.Count == 0)
        {
            return;
        }
        else
        {
            foreach (GameObject go in targetList)
            {
                distance = (go.transform.position - transform.position).sqrMagnitude;
                if (minDis > distance)
                {
                    minDis = distance;
                    _lockTarget = go;
                }
            }
        }
    }
}

