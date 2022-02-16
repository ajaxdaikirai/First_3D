using System.Collections;
using System.Collections.Generic;
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

        //소환창에 캐릭터 버튼을 추가
        //==============수정 필요====================
        //차후 보유 캐릭터를 불러와서 버튼 생성하도록 수정
        _unitItems[0] = "FitnessGirlSniper";
        _unitItems[1] = "OfficeGirlKnight";
        foreach(string unitItem in _unitItems)
        {
            UIItemSummonUnit uiItem = Managers.UI.MakeSubItem<UIItemSummonUnit>(unitSummonPanel.transform);
            if (!string.IsNullOrEmpty(unitItem))
            {
                uiItem.SetName(unitItem);
            }
        }
        
    }

}
