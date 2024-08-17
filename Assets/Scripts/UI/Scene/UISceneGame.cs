using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISceneGame : UIScene
{
    enum Images
    {
        JoyStick,
        Handle,
    }

    enum Buttons
    {
        AttackBtn,
    }

    enum Object
    {
        UnitSummonPanel,
        EnemySummonPanel,
        Panel_GameOver,
    }

    enum Object_Enemy
    {
        EnemySummonPanel,
    }

    //�̹����� ����� ���� ����
    string[] _enemyItems = new string[4];
    string[] _unitItems = new string[2];

    GameObject unitSummonPanel;

    public override void Init()
    {
        //���̽�ƽ�� �ڵ鷯 �߰�
        Bind<Image>(typeof(Images));
        BindJoyStickEvent(GetImage((int)Images.JoyStick).gameObject, GetImage((int)Images.Handle).gameObject);
        
        //�⺻���� ��ư
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.AttackBtn).gameObject, (PointerEventData data) => AttackEvent(data));

        //ĳ���� ��ȯâ
        Bind<GameObject>(typeof(Object));
        GameObject unitSummonPanel = Get<GameObject>((int)Object.UnitSummonPanel);

        //�� ��ȯâ
        //Bind<GameObject>(typeof(Object_Enemy));
        GameObject enemySummonPanel = Get<GameObject>((int)Object.EnemySummonPanel);



        //��ȯâ�� ĳ���� ��ư�� �߰�
        //==============���� �ʿ�====================
        //���� ���� ĳ���͸� �ҷ��ͼ� ��ư �����ϵ��� ����
        _unitItems[0] = "FitnessGirlSniper";
        _unitItems[1] = "OfficeGirlKnight";
        _enemyItems[0] = "Green";
        _enemyItems[1] = "Blue";
        _enemyItems[2] = "Red";
        _enemyItems[3] = "Purple";

        foreach (string unitItem in _unitItems)
        {
            UIItemSummonUnit uiItem = Managers.UI.MakeSubItem<UIItemSummonUnit>(unitSummonPanel.transform);
            if (!string.IsNullOrEmpty(unitItem))
            {
                uiItem.SetName(unitItem);
            }
        }

        foreach (string enemyItem in _enemyItems)
        {
            UIItemSummonEnemy uiEnemyItem = Managers.UI.MakeSubItem<UIItemSummonEnemy>(enemySummonPanel.transform);
            if (!string.IsNullOrEmpty(enemyItem))
            {
                uiEnemyItem.SetName(enemyItem);
            }
        }

    }
    public void Update()
    {
        GameObject unitSummonPanel = Get<GameObject>((int)Object.UnitSummonPanel);
        UnitSpawningPool uSp = GameObject.Find("EnemySpawningPool").GetComponent<UnitSpawningPool>();
        int keepCount = uSp.GetKeepObjectCount();
        int unitCount = uSp.GetUnitCount();
        int maxCount = uSp.GetMaxUnitCount();
        
        if (maxCount == Managers.Game._summonedUnitCount)
        {
            unitSummonPanel.transform.gameObject.SetActive(false);
            return;
        }
        else if (keepCount + 1 <= unitCount)
        {
            unitSummonPanel.transform.gameObject.SetActive(false);
        }
        else if (keepCount + 1 > unitCount)
        {
            unitSummonPanel.transform.gameObject.SetActive(true);
        }
    }

}
