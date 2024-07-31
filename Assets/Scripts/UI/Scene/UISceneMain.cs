using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISceneMain : UIScene
{
    enum Buttons
    {
        GameStartBtn,
    }

    enum Texts
    {
        GameStartTxt,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        BindEvent(GetButton((int)Buttons.GameStartBtn).gameObject, LoadGameScene);
    }

    //æ¿ ¿Ãµø
    public void LoadGameScene(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scenes.GameScene);
    }
}
