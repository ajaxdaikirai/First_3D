using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemUnitUpgrade : UIBase
{
    enum Buttons
    {
        UnitUpgradeBtn,
    }

    enum Texts
    {
        UnitUpgradeTxt,
        UnitName,
    }

    // 유닛 이미지가 생길때까지 임시로 이름을 표시
    // ======================차후 수정==================
    string _name;

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        GetText((int)Texts.UnitName).text = _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }
}
