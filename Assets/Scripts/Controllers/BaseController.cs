using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    //현재 상태
    [SerializeField]
    protected Define.State _state = Define.State.Idle;

    //락온된 오브젝트
    [SerializeField]
    protected GameObject _lockTarget;

    //애니메이터
    protected Animator _anim;

    //목적지
    protected Vector3 _destPos;

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
                    break;
                case Define.State.Attack:
                    break;
                case Define.State.Moving:
                    _anim.CrossFade("Walk", 0.07f);
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

    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateAlways() { }
}
