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

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        BindJoyStickEvent(GetImage((int)Images.JoyStick).gameObject, GetImage((int)Images.Handle).gameObject);
        
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.AttackBtn).gameObject, (PointerEventData data) => BindAttackEvent(data));
    }
   
}
