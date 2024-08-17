using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameScene_Panel : UIBase
{

    enum Buttons
    {
        GameStart_btn,
        main_btn,
    }
    private void Awake()
    {
        transform.gameObject.SetActive(false); // ������ ���۵Ǹ� GameOver �˾� â�� ������ �ʵ��� �Ѵ�.
    }

    public void Show()
    {
        new WaitForSeconds(Define.RETRY_DELAY_TIME);
        Time.timeScale = 0;
        transform.gameObject.SetActive(true);
    }

    public void OnClick_Game_Start() // 'start' ��ư�� Ŭ���ϸ� ȣ�� �Ǿ��� �Լ�
    {

        //�÷��̾� ����
        //GameObject player = Managers.Game.InstantiatePlayer();


        //ī�޶� ����
        Util.GetOrAddComponent<CameraController>(Camera.main.gameObject).SetPlayer(Managers.Game.Player);


        Time.timeScale = 1;
        Awake();
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
        BindEvent(GetButton((int)Buttons.GameStart_btn).gameObject, (PointerEventData data) => OnClick_Game_Start());
        BindEvent(GetButton((int)Buttons.main_btn).gameObject, (PointerEventData data) => OnClick_Main());

    }
}
