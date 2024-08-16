using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopupStageClear : UIPopup
{
    enum Buttons
    {
        NextStageBtn,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.NextStageBtn).gameObject, (PointerEventData data) => LoadPrepareScene(data));
    }

    // 준비 씬으로 이동
    public void LoadPrepareScene(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scenes.PrepareScene);
    }
}
