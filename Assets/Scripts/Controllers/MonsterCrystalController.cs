using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCrystalController : StaticObjectController
{
    //����
    Stat _stat;

    //���ܰ� Ŭ����
    int allClear;

    //�ؽ�Ʈ �������� �ǳ�
    Panel_NextStage panel_NextStage;
    //��Ŭ���� �ǳ�
    All_Clear_Panel panel_AllClear;

    public override void Init()
    {

        _stat = gameObject.GetComponent<Stat>();
        if (_stat == null)
        {
            Debug.Log("Can't Load Stat Component");
        }
        _stat.SetStat(Managers.Data.GetStatByLevel("MonsterCrystalStat", 1));


        //HP�� �߰�
        if (gameObject.GetComponentInChildren<UIHpBar>() == null)
        {
            Managers.UI.MakeWorldUI<UIHpBar>(transform);
        }

        panel_NextStage = Managers.Game.NextPanel;
        panel_AllClear = Managers.Game.ClearPanel;
    }

    public override void Destroy()
    {
        Conf.Main.CLEAR_FLAG = true;
        transform.gameObject.SetActive(false);
        allClear = Conf.Main.CURRENT_STAGE;
        switch (allClear)
        {
            case 4:
                panel_AllClear.Show();
                break;

            default:
                panel_NextStage.Show();
                break;

        }
        //Managers.Resource.Destroy(gameObject);
    }
}
