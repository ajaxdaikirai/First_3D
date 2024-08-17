using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

public class Panel_GameOver : UIBase
{
    enum Buttons
    {
        retry_btn,
        main_btn,
    }
    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� GameOver �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.RETRY_DELAY_TIME);
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Retry() // '�絵��' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {
        Awake();
        SceneManagerEx scene = Managers.Scene;
        switch (Conf.Main.CURRENT_SCENE)
        {
            case 1:
                scene.LoadScene(Define.Scenes.GameSceneStage1); // SceneManager�� LoadScene �Լ��� ����Ͽ�! ���� ���� �ٽ� �ҷ������� ��Ų��.
                                   // ���� ���� �ٽ� �ҷ����� ������ ����� �ȴ�.
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
        }
        

    }

    private string Concat(string sCENE_NAME, int cURRENT_SCENE)
    {
        throw new NotImplementedException();
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
        Awake();

        BindEvent(GetButton((int)Buttons.retry_btn).gameObject, (PointerEventData data) => OnClick_Retry());
        BindEvent(GetButton((int)Buttons.main_btn).gameObject, (PointerEventData data) => OnClick_Main());
    }

}
