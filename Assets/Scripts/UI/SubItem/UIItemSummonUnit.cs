using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemSummonUnit : UIBase
{
    
    enum Buttons
    {
        UIItemSummonUnit,
    }

    enum Texts
    {
        UIItemSummonUnitTxt,
    }

    //���� �̹����� ���涧���� �ӽ÷� �̸��� ǥ��
    //======================���� ����==================
    string _name;

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.UIItemSummonUnitTxt).text = _name;



        BindEvent(GetButton((int)Buttons.UIItemSummonUnit).gameObject, (PointerEventData data) => SummonUnit(data));
    }

    public void Update()
    {

    }
    public void SetName(string name)
    {
        _name = name;
    }

    public void SummonUnit(PointerEventData data)
    {

        Managers.Game.Spawn(Define.Layer.Unit, $"Units/{_name}");

    }
}
