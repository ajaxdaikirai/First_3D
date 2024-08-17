using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class All_Clear_Panel : UIBase
{
    enum Buttons
    {
        main_btn,
    }
    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Main()
    {
        Awake();
        SceneManagerEx scene = Managers.Scene;
        scene.LoadScene(Define.Scenes.MainScene);// ���ξ����� ���ư���

        Conf.Main.CLEAR_FLAG = false;
        Conf.Main.CURRENT_STAGE = 1;
        Conf.Main.CURRENT_SCENE = 1;

    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        //Awake();

        BindEvent(GetButton((int)Buttons.main_btn).gameObject, (PointerEventData data) => OnClick_Main());
    }
}
