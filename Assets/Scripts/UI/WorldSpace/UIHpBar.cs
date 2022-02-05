using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : UIBase
{
    //hp바 위치
    [SerializeField]
    Vector3 _hpPos = new Vector3(0, 0.5f, 0);

    //대상 오브젝트
    Transform parent;

    //대상 오브젝트 스텟
    Stat _stat;

    //대상 오브젝트 높이
    float _parentHeight;

    enum GameObjects
    {
        HpBar,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
        
        //대상 오브젝트 높이값 취득
        parent = transform.parent;
        _parentHeight = parent.GetComponent<Collider>().bounds.size.y;
    }

    private void Update()
    {
        transform.position = parent.position + Vector3.up * _parentHeight + _hpPos;
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetGameObject((int)GameObjects.HpBar).GetComponent<Slider>().value = ratio;
    }

}
