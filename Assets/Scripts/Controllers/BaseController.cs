using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    //현재 상태
    [SerializeField]
    private Define.State _state = Define.State.Idle;

    //방향
    protected Vector3 _dir = Vector3.forward;
    //리지드바디
    protected Rigidbody _rig;
    //애니메이터
    protected Animator _anim;
    //스텟
    protected Stat _stat;
    //목적지
    protected Vector3 _destPos;
    // 쿨타임 플래그
    protected bool _attackFlag = true;
    // 생존 플래그
    protected bool _aliveFlag = true;

    public virtual Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;

            if (_anim == null) return;

            switch (_state)
            {
                //애니메이션 재생
                case Define.State.Idle:
                    _anim.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Die:
                    _anim.CrossFade(DieAnimName(), 0.5f);
                    break;
                case Define.State.Attack:
                    _anim.CrossFade("Attack", 0.1f);
                    break;
                case Define.State.Moving:
                    _anim.CrossFade("Walk", 0.1f);
                    break;
                case Define.State.Skill:
                    break;
            }
        }
    }

    // ================================
    // 애니메이션 관련
    // ================================
    // Die 상태의 애니메이션명
    protected virtual string DieAnimName() { return "Die"; }

    private void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        switch (State)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Die:
                UpdateDie();
                break;
            case Define.State.Moving:
                UpdateMoving();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
            case Define.State.Skill:
                UpdateSkill();
                break;

        }
        UpdateAlways();
    }

    // 애니메이션이 있는 오브젝트 디폴트 설정
    protected void SetCreatureDefault()
    {
        //상태 초기화
        State = Define.State.Idle;

        //애니메이터
        _anim = gameObject.GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Can't Load Animator Component");
        }

        //리지드바디
        _rig = gameObject.GetComponent<Rigidbody>();
        if (_anim == null)
        {
            Debug.Log("Can't Load Rigidbody Component");
        }

        //스텟 추가
        _stat = transform.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel($"{gameObject.name}Stat", 1));

        //HP바 추가
        if (gameObject.GetComponentInParent<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }
    }

    protected virtual void UpdateAlways() 
    {
        _rig.velocity = Vector3.zero; 
    }

    protected virtual void UpdateDie() 
    {
        if (_aliveFlag)
        {
            _aliveFlag = false;
            Managers.Game.Despawn((int)Define.Layer.Unit, gameObject);
        }
    }

    // 쿨타임 경과후 공격가능 플래그 활성
    protected IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(_stat.AttackSpeed);
        _attackFlag = true;
    }

    // 공격 후의 처리
    // 애니메이션에서 호출되는 경우가 있음
    void EndAttack()
    {
        State = Define.State.Idle;
    }

    protected virtual void OnEnable()
    {
        //상태 초기화
        State = Define.State.Idle;

        //체력회복
        if (_stat != null)
        {
            _stat.Hp = _stat.MaxHp;
        }

        _aliveFlag = true;
    }

    public virtual void OnDie()
    {
        State = Define.State.Die;
    }

    // ====================================
    // 추상 메서드
    // ====================================
    protected abstract void Init();

    // ====================================
    // 필수는 아니지만
    // 오버라이드 해야만 사용가능한 메서드
    // ====================================
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateMoving() { }
}
