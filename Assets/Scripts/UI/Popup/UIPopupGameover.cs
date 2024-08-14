using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupGameover : UIPopup
{
    enum Buttons
    {
        MainSceneBtn,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.MainSceneBtn).gameObject, (PointerEventData data) => LoadMainScene(data));
    }

    // 메인씬으로 이동
    public void LoadMainScene(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scenes.MainScene);
    }
}
