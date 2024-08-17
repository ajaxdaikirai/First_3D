using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    //�����ǳ� �ʱ�ȭ
    Main_Panel main_Panel;

    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scenes.MainScene;

        Managers.UI.ShowSceneUI<UISceneMain>();

        //�� ������Ʈ �̸� ����
        gameObject.name = System.Enum.GetName(typeof(Define.Scenes), _sceneType);

        main_Panel = Managers.Game.Main_Panel;

        main_Panel.Show();

    }

    public override void Clear()
    {
    }
}
