using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISceneGame : UIScene
{
    enum Images
    {
        JoyStick,
        Handle,
    }

    enum Buttons
    {
        AttackBtn,
    }

    enum Objects
    {
        UnitSummonPanel,
    }

    //이미지가 생기면 차후 수정
    string[] _unitItems = new string[5];

    public override void Init()
    {
        //조이스틱에 핸들러 추가
        Bind<Image>(typeof(Images));
        BindJoyStickEvent(GetImage((int)Images.JoyStick).gameObject, GetImage((int)Images.Handle).gameObject);
        
        //기본공격 버튼
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.AttackBtn).gameObject, (PointerEventData data) => AttackEvent(data));

        //캐릭터 소환창
        Bind<GameObject>(typeof(Objects));
        GameObject unitSummonPanel = Get<GameObject>((int)Objects.UnitSummonPanel);

        foreach (CharacterConf.Unit unit in Enum.GetValues(typeof(CharacterConf.Unit)))
        {
            UIItemSummonUnit item = Managers.UI.MakeSubItem<UIItemSummonUnit>(unitSummonPanel.transform);
            item.SetName(unit.ToString());
        }

    }

}
