using System;
using UnityEngine;
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
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(Objects));
        Bind<Text>(typeof(Texts));

        // 스테이지ID 출력
        Text stageIdTxt = Get<Text>((int)Texts.StageIdTxt);
        stageIdTxt.text = $"Stage{Managers.Status.StageId}";

        GameObject unitListPanel = Get<GameObject>((int)Objects.UnitListPanel);
        
        foreach (CharacterConf.Unit unit in Enum.GetValues(typeof(CharacterConf.Unit)))
        {
            UIItemUnitUpgrade item = Managers.UI.MakeSubItem<UIItemUnitUpgrade>(unitListPanel.transform);
            item.SetName(unit.ToString());
            item.SetUnitId((int)unit);
        }
    }
}
