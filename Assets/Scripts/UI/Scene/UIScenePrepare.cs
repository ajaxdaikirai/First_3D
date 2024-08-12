using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScenePrepare : UIScene
{
    enum Objects
    {
        UnitListPanel,
    }

    enum Texts
    {
        StageIdTxt,
        UpgradePoint,
    }

    enum Buttons
    {
        NextStageBtn,
    }

    List<UIItemUnitUpgrade> _upgradeItemUIs = new List<UIItemUnitUpgrade>();

    public override void Init()
    {
        Bind<GameObject>(typeof(Objects));
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        // 스테이지ID 출력
        Text stageIdTxt = Get<Text>((int)Texts.StageIdTxt);
        stageIdTxt.text = $"Stage{Managers.Status.StageId}";

        GameObject unitListPanel = Get<GameObject>((int)Objects.UnitListPanel);

        foreach (CharacterConf.Unit unit in Enum.GetValues(typeof(CharacterConf.Unit)))
        {
            UIItemUnitUpgrade item = Managers.UI.MakeSubItem<UIItemUnitUpgrade>(unitListPanel.transform);
            // 서브UI를 이곳에서 관리 
            _upgradeItemUIs.Add(item);

            item.SetName(unit.ToString());
            item.SetUnitId((int)unit);
            item.SetParent(this);
        }

        BindEvent(GetButton((int)Buttons.NextStageBtn).gameObject, LoadGameScene);

        // 스킬 포인트
        UpdatePoint();
    }

    //씬 이동
    public void LoadGameScene(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scenes.GameScene);
    }

    // 업그레이드를 실행했을 때의 처리
    public void ExecUpgrade()
    {
        UpdatePoint();
        UpdateSubItemUIs();
    }

    // 스킬 포인트 표시 갱신
    public void UpdatePoint()
    {
        Get<Text>((int)Texts.UpgradePoint).text = Managers.Status.Point.ToString();
    }

    // 각 UI아이템들을 갱신함
    public void UpdateSubItemUIs()
    {
        if (Managers.Status.Point > 0)
            return;

        foreach (UIItemUnitUpgrade ui in _upgradeItemUIs)
        {
            ui.UpdateElements();
        }
    }
}
