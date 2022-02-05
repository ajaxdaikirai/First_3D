using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : BaseController
{
    //플레이푳E컨트롤러 UI가 들어있는 컴포넌트
    UIScene _uiScene;

    //조이스틱 방향
    Vector3 _dir;

    //플레이어 스텟
    Stat _stat;

    Rigidbody _rig;

    public override void Init()
    {
        //상태 초기화
        _state = Define.State.Idle;

        //스텟 초기화
        _stat = gameObject.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }

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

        //컨트롤러UI 초기화
        _uiScene = Managers.UI.UIScene;

        if(_uiScene == null || _uiScene.JoyStickHandler == null)
        {
            Debug.Log("Not Exist Player Controller UI");
        }

        //HP바 추가
        if (gameObject.GetComponentInChildren<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }

        //버튼 액션 추가
        AddAction();
    }

    //Invoke로 사용할 수 있게 각종 버튼에 액션 추가
    void AddAction()
    {
        _uiScene.JoyStickHandler.OnDragHandler -= OnJoyStickDragEvent;
        _uiScene.JoyStickHandler.OnDragHandler += OnJoyStickDragEvent;
        _uiScene.JoyStickHandler.OnUpHandler -= OnJoyStickUpEvent;
        _uiScene.JoyStickHandler.OnUpHandler += OnJoyStickUpEvent;
    }

    //조이스틱의 방향을 인자로 받음
    void OnJoyStickDragEvent(Vector3 diretion)
    {
        
        State = Define.State.Moving;       
        _dir = diretion;
    }

    void OnJoyStickUpEvent()
    {
        State = Define.State.Idle;
    }

    protected override void UpdateMoving()
    {
        transform.position += _dir * Time.deltaTime * _stat.MoveSpeed;
        if (_dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10 * Time.deltaTime);
    }

    protected override void UpdateAlways()
    {
        _rig.velocity = Vector3.zero;
    }
}
