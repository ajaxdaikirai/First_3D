using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : BaseController
{
    //�÷��̾�E��Ʈ�ѷ� UI�� ����ִ� ������Ʈ
    UIScene _uiScene;

    //��ų �߻� ����
    Transform _launchPoint;

    //���ݴ�� ���̾�
    int[] _layers;

    //��ų �̸�
    string SKILL_NAME = "PlayerAttack";

    //���ӿ��� �ǳ��ʱ�ȭ
    Panel_GameOver panel_GameOver;

    public override void Init()
    {
        //���� �ʱ�ȭ
        State = Define.State.Idle;

        //Ŭ���� �÷����ʱ�ȭ
        Conf.Main.CLEAR_FLAG = false;

        //���� �ʱ�ȭ
        _stat = gameObject.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel("PlayerStat", 1));

        //�ִϸ�����
        _anim = gameObject.GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Can't Load Animator Component");
        }

        //������ٵ�
        _rig = gameObject.GetComponent<Rigidbody>();
        if (_rig == null)
        {
            Debug.Log("Can't Load Rigidbody Component");
        }

        //��Ʈ�ѷ�UI �ʱ�ȭ
        _uiScene = Managers.UI.UIScene;

        if (_uiScene == null || _uiScene.JoyStickHandler == null)
        {
            Debug.Log("Not Exist Player Controller UI");
        }

        //HP�� �߰�
        if (gameObject.GetComponentInChildren<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }

        panel_GameOver = Managers.Game.Panel;
        //���ӿ��� �ǳ��߰�
        if (panel_GameOver == null)
        {
            Debug.Log("Not Exist GameOver Panel");
        }


        //��ų �߻� ����
        _launchPoint = Util.FindChild<Transform>(gameObject, "LaunchPoint", true);

        //���ݴ�� ���̾� ����
        _layers = new int[]{ (int)Define.Layer.Monster, (int)Define.Layer.EnemyStaticObject};

        //��ư �׼� �߰�
        AddAction();
    }

    //Invoke�� ����� �� �ְ� ���� ��ư�� �׼� �߰�
    void AddAction()
    {
        _uiScene.JoyStickHandler.OnDragHandler -= OnJoyStickDragEvent;
        _uiScene.JoyStickHandler.OnDragHandler += OnJoyStickDragEvent;
        _uiScene.JoyStickHandler.OnUpHandler -= OnJoyStickUpEvent;
        _uiScene.JoyStickHandler.OnUpHandler += OnJoyStickUpEvent;
        _uiScene.OnAttackBtnDownHandler -= OnAttackBtnDownEvent;
        _uiScene.OnAttackBtnDownHandler += OnAttackBtnDownEvent;
    }

    //���̽�ƽ�� ������ ���ڷ� ����
    void OnJoyStickDragEvent(Vector3 diretion)
    {
        if (State != Define.State.Moving)
        {
            State = Define.State.Moving;
        }
        _dir = diretion;

    }

    void OnJoyStickUpEvent()
    {
        State = Define.State.Idle;
    }

    //���ݹ�ư Ŭ����
    void OnAttackBtnDownEvent()
    {
        if (_attackFlag)
        {
            State = Define.State.Attack;
            _attackFlag = false;
            StartCoroutine(AttackCoolTime());
        }

    }

    //���� ��Ÿ�� ���� ���� �÷��׸� false
    protected IEnumerator AttackCoolTime()
    {
        yield return new WaitForSeconds(_stat.AttackSpeed);
        _attackFlag = true;
    }

    void OnAttack()
    {
        Managers.Skill.SpawnSkill(SKILL_NAME, _launchPoint.position, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, _layers);
    }

    void EndAttack()
    {
        State = Define.State.Idle;
    }

    protected override void UpdateMoving()
    {
        transform.position += _dir * Time.deltaTime * _stat.MoveSpeed;
        _dir.y = 0;
        if (_dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10 * Time.deltaTime);

    }

    protected override void UpdateDie()
    {
        _aliveFlag = false;
        StartCoroutine(Despwn());
        panel_GameOver.Show();
    }

    protected override void UpdateClear()
    {
        State = Define.State.Clear;
        base.UpdateClear();
    }

    protected override IEnumerator Despwn()
    {
        yield return new WaitForSeconds(Define.DESPAWN_DELAY_TIME);
        Managers.Game.Despawn(Define.Layer.Player, gameObject);
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
