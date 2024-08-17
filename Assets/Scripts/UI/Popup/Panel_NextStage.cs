using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Panel_NextStage : UIBase
{

    enum Buttons
    {
        next_btn,
        main_btn,
    }
    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� NestStage �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.NEXT_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Next() // '���� ����������' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        Conf.Main.CURRENT_STAGE++;
        Conf.Main.CLEAR_FLAG = false;

        Managers.Game.Units.Clear();
        Managers.Game.Monsters.Clear();

        Awake();
        SceneManagerEx scene = Managers.Scene;
        switch (Conf.Main.CURRENT_STAGE)
        {
            case 1:
                scene.LoadScene(Define.Scenes.GameSceneStage1); 
                break;
            case 2:
                scene.LoadScene(Define.Scenes.GameSceneStage2);
                break;
            case 3:
                scene.LoadScene(Define.Scenes.GameSceneStage3);
                break;
            case 4:
                scene.LoadScene(Define.Scenes.GameSceneStage4);
                break;
            default:
                scene.LoadScene(Define.Scenes.MainScene);
                break;
        }

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

        BindEvent(GetButton((int)Buttons.next_btn).gameObject, (PointerEventData data) => OnClick_Next());
        BindEvent(GetButton((int)Buttons.main_btn).gameObject, (PointerEventData data) => OnClick_Main());
    }
}
