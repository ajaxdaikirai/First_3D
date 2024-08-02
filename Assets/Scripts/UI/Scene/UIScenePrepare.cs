using System;
using UnityEngine;

public class UIScenePrepare : UIScene
{
    enum Objects
    {
        UnitListPanel,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(Objects));
        GameObject unitListPanel = Get<GameObject>((int)Objects.UnitListPanel);
        
        foreach (CharacterConf.Unit unit in Enum.GetValues(typeof(CharacterConf.Unit)))
        {
            UIItemUnitUpgrade item = Managers.UI.MakeSubItem<UIItemUnitUpgrade>(unitListPanel.transform);
            item.SetName(unit.ToString());
        }
    }
}
