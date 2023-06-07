using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    //현재 상태
    [SerializeField]
    private Define.State _state = Define.State.Idle;

    //락온된 오브젝트
    [SerializeField]
    protected GameObject _lockTarget;

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

    protected bool _attackFlag = true;
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
                    int ranNom = (int)Random.Range(1.0f, 4.0f);
                    _anim.CrossFade($"Die{ranNom}", 0.5f);
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

    public abstract void Init();

    protected virtual void UpdateAlways() 
    {
        _rig.velocity = Vector3.zero; 
    }

    protected virtual void UpdateDie() 
    {
        if (_aliveFlag)
        {
            _aliveFlag = false;
            StartCoroutine(Despwn());
        }
    }

    protected virtual void UpdateIdle() {}
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateMoving() { }

    //사망시 일정시간 후 비활성화
    protected IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);
        Managers.Game.Despawn(Define.Layer.Unit, gameObject);
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
}
