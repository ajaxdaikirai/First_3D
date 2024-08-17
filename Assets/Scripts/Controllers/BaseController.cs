using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    //���� ����
    [SerializeField]
    private Define.State _state = Define.State.Idle;

    //���µ� ������Ʈ
    [SerializeField]
    protected GameObject _lockTarget;

    //����
    protected Vector3 _dir = Vector3.forward;

    //������ٵ�
    protected Rigidbody _rig;

    //�ִϸ�����
    protected Animator _anim;

    //����
    protected Stat _stat;

    //������
    protected Vector3 _destPos;

    protected bool _attackFlag = true;
    protected bool _aliveFlag = true;
    protected bool _unitFlag = true;
    protected bool _monsterFlag = true;
    protected bool _continuedFlag = true;
    protected bool _stopFlag = false;



    public virtual Define.State State
    {   
        get { return _state; }
        set
        {
            _state = value;

            if (_anim == null || _stopFlag) return;
            
            switch (_state)
            {
                //�ִϸ��̼� ���
                case Define.State.Idle:
                    _anim.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Die:
                    //int ranNom = (int)Random.Range(1.0f, 4.0f);
                    _anim.CrossFade($"Die", 0.05f);
                    break;
                case Define.State.Attack:
                    _anim.CrossFade("Attack", 0.1f);
                    break;
                case Define.State.Moving:
                    _anim.CrossFade("Walk", 0.1f);
                    break;
                case Define.State.Skill:
                    break;
                case Define.State.Clear:
                    _anim.CrossFade("Clear", 0.3f);
                    _stopFlag = true;
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
        if (Conf.Main.CLEAR_FLAG)
            State = Define.State.Clear;
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
            case Define.State.Clear:
                UpdateClear();
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

        StartCoroutine(Despwn());

    }

    protected virtual void UpdateIdle() {}
    protected virtual void UpdateAttack() { }
    protected virtual void UpdateSkill() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateClear() { }

    //����� �����ð� �� ��Ȱ��ȭ
    protected virtual IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);

/*        if (!_aliveFlag)
        {
            Managers.Game.Despawn(Define.Layer.Player, gameObject);
        }
        else if (!_monsterFlag)
        {
            Managers.Game.Despawn(Define.Layer.Monster, gameObject);
            _monsterFlag = true;
        }
        else if (!_unitFlag)
        {
            Managers.Game.Despawn(Define.Layer.Unit, gameObject);
            _unitFlag = true;
        }*/

    }

    protected virtual void OnEnable()
    {
        //���� �ʱ�ȭ
        State = Define.State.Idle;

        //ü��ȸ��
        if (_stat != null)
        {
            _stat.Hp = _stat.MaxHp;
        }

        _aliveFlag = true;
    }
}
