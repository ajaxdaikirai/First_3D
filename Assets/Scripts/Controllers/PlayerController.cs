using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : BaseController
{
    //플레이푳E컨트롤러 UI가 들어있는 컴포넌트
    UIScene _uiScene;

    //스킬 발사 지점
    Transform _launchPoint;

    //공격대상 레이어
    int[] _layers;

    //스킬 이름
    string SKILL_NAME = "PlayerAttack";

    protected override void Init()
    {
        SetCreatureDefault();

        //컨트롤러UI 초기화
        _uiScene = Managers.UI.UIScene;

        if (_uiScene == null || _uiScene.JoyStickHandler == null)
        {
            Debug.Log("Not Exist Player Controller UI");
        }

        //스킬 발사 지점
        _launchPoint = Util.FindChild<Transform>(gameObject, "LaunchPoint", true);

        //공격대상 레이어 지정
        _layers = new int[]{ (int)Define.Layer.Monster, (int)Define.Layer.EnemyStaticObject};

        //버튼 액션 추가
        AddAction();
    }

    protected override string DieAnimName()
    {
        return $"Die{Random.Range(1, 5)}";
    }

    //Invoke로 사용할 수 있게 각종 버튼에 액션 추가
    void AddAction()
    {
        _uiScene.JoyStickHandler.OnDragHandler -= OnJoyStickDragEvent;
        _uiScene.JoyStickHandler.OnDragHandler += OnJoyStickDragEvent;
        _uiScene.JoyStickHandler.OnUpHandler -= OnJoyStickUpEvent;
        _uiScene.JoyStickHandler.OnUpHandler += OnJoyStickUpEvent;
        _uiScene.OnAttackBtnDownHandler -= OnAttackBtnDownEvent;
        _uiScene.OnAttackBtnDownHandler += OnAttackBtnDownEvent;
    }

    //조이스틱의 방향을 인자로 받음
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

    //공격버튼 클릭시
    void OnAttackBtnDownEvent()
    {
        if (_attackFlag)
        {
            State = Define.State.Attack;
            _attackFlag = false;
            StartCoroutine(AttackCoolTime());
        }
    }

    void OnAttack()
    {
        Managers.Skill.SpawnSkill(SKILL_NAME, _launchPoint.position, _dir, _stat.AttackDistance, _stat.ProjectileSpeed, _stat.Offence, _layers);
    }

    protected override void UpdateMoving()
    {
            transform.position += _dir * Time.deltaTime * _stat.MoveSpeed;
            if (_dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10 * Time.deltaTime);
    }

    public override void OnDie()
    {
        if (!_aliveFlag)
            return;

        State = Define.State.Die;
        _aliveFlag = false;

        // 사망 후에는 뒤의 캐릭터에 방해가 되지 않도록 콜라이더를 해제
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        // 게임오버 판넬 활성
        Managers.Game.Gameover();
    }

}
