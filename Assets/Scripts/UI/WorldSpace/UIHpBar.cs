using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : UIBase
{
    //hp�� ��ġ
    [SerializeField]
    Vector3 _hpPos = new Vector3(0, 0.3f, 0);

    //��� ������Ʈ
    Transform _parent;

    //��� ������Ʈ ����
    Stat _stat;

    //��� ������Ʈ ����
    float _parentHeight;

    enum GameObjects
    {
        UIHpBar,
        HpBar,
    }

    enum Images
    {
        Fill,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        _stat = transform.parent.GetComponent<Stat>();

        _parent = transform.parent;

        //��� ������Ʈ ���̰� ���
        _parentHeight = _parent.GetComponent<Collider>().bounds.size.y;

        GameObject go = GetImage((int)Images.Fill).gameObject;

        //hp�� ����
        if (_parent.gameObject.layer == (int)Define.Layer.Unit)
        {

            GetImage((int)Images.Fill).GetComponent<Image>().color = new Color(84 / 255f, 153 / 255f, 1);

        }
        else if (_parent.gameObject.layer == (int)Define.Layer.Monster)
        {

            GetImage((int)Images.Fill).GetComponent<Image>().color = new Color(84 / 255f, 153 / 255f, 1);

        }
        else if (_parent.gameObject.layer == (int)Define.Layer.EnemyStaticObject)
        {
            GetImage((int)Images.Fill).GetComponent<Image>().color = new Color(199 / 255f, 83 / 255f, 1);
            //GetGameObject((int)GameObjects.UIHpBar).transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if(_parent.gameObject.layer == (int)Define.Layer.Player)
        {
            GetImage((int)Images.Fill).GetComponent<Image>().color = new Color(117 / 255f, 1, 84 / 255f);
        }
    }

    private void Update()
    {
        transform.SetPositionAndRotation(_parent.position + Vector3.up * _parentHeight + _hpPos, Camera.main.transform.rotation);

        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetGameObject((int)GameObjects.HpBar).GetComponent<Slider>().value = ratio;
    }

}
