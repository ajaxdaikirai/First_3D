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

    //유닛 이미지가 생길때까지 임시로 이름을 표시
    //======================차후 수정==================
    string _name;
    int _unitId;

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.UIItemSummonUnitTxt).text = _name;
        BindEvent(GetButton((int)Buttons.UIItemSummonUnit).gameObject, (PointerEventData data) => SummonUnit(data));
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetUnitId(int unitId)
    {
        _unitId = unitId;
    }

    // 소환
    public void SummonUnit(PointerEventData data)
    {
        Managers.Game.SummonUnit(_unitId);
    }
}
